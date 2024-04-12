using Application.Exceptions;
using Application.Tariff;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Tariff;
using FluentAssertions;
using Moq;

namespace Tests.Tariff;

public sealed class TariffServicesTest : TestBase
{
    private ITariffService TariffService { get; set; }

    public TariffServicesTest()
    {
        TariffService = FixtureGenerator.Create<TariffService>();
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет список тарифов c определенного индекса и указанной длины")]
    public async Task GetAllAsync_ValidInput_ReturnsListOfTariffActionsFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<TariffRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<TariffRecord>());
        }

        TariffRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        TariffRepositoryMock.Setup(m => m.GetListAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await TariffService.GetListAsync(offset, limit);

        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                          "если смещение превышает общее количество элементов")]
    public async Task GetAllAsync_OffsetExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(6, 100);

        TariffRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => TariffService.GetListAsync(0, offset);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("offset exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                        "если страница превышает общее количество элементов")]
    public async Task GetAllAsync_PageExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(3, 100);
        var limit = new Random().Next(3, 100);
        TariffRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => TariffService.GetListAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать запись по её Id, если она существует в БД")]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleTariff = FixtureGenerator.Create<TariffRecord>();

        TariffRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleTariff.Id)).ReturnsAsync(sampleTariff);

        //Act
        var entity = await TariffService.GetByIdAsync(sampleTariff.Id);

        //Assert
        entity.Should().BeEquivalentTo(sampleTariff.ToGetModel());
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать NotFoundException при вводе отсутствующего в базе Id")]
    public void GetByIdAsync_NonexistentInDbId_ShouldReturnNotFoundException()
    {
        //Act
        var action =  () =>  TariffService.GetByIdAsync(Guid.NewGuid());

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync должен возвращать Id добавленной в БД записи")]
    public async Task AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = FixtureGenerator.Create<AddTariffModel>();

        TariffRepositoryMock.Setup(m => m.AddAsync(It.IsAny<TariffRecord>()))
            .ReturnsAsync(Guid.NewGuid());

        //Act
        var resultId = await TariffService.AddAsync(model);

        //Assert
        TariffRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<TariffRecord>(v => v.VerifyBy(model))));

        resultId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Метод UpdateAsync должен возвращать обновляемую в БД модель")]
    public async Task UpdateAsync_ValidInput_UpdateTariffModel()
    {
        //Arrange
        var updateModel = FixtureGenerator.Create<UpdateTariffModel>();

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<TariffRecord>());

        TariffRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<TariffRecord>()));

        //Act
        var result = await TariffService.UpdateAsync(updateModel);

        //Assert
        result.Should().Be(updateModel);
    }

    [Fact(DisplayName = "Метод DeleteAsync должен возвращать булево значение, означающее успешность операции удаления записи")]
    public async Task DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<TariffRecord>();

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        TariffRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);

        //Act
        var result = await TariffService.DeleteAsync(sampleModel.Id);

        //Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = "Метод Delete должен вернуть NotFoundException при вводе Id несуществующей записи")]
    [AutoData]
    public async Task DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Act
        var action = () => TariffService.DeleteAsync(id);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
