using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Tariffication;
using SkiiResort.Application.Tariffication.Models;
using SkiiResort.Domain.Entities.Tariffication;

namespace SkiiResort.Tests.Tariffication;

public sealed class TarifficationServicesTest : TestBase
{
    private ITarifficationService TarifficationService { get; set; }

    public TarifficationServicesTest()
    {
        TarifficationService = FixtureGenerator.Create<TarifficationService>();
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет список тариффикаций c определенного индекса и указанной длины")]
    public async Task GetAllAsync_ValidInput_ReturnsListOfTarifficationFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<TarifficationRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<TarifficationRecord>());
        }

        TarifficationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        TarifficationRepositoryMock.Setup(m => m.GetAllAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await TarifficationService.GetAllAsync(offset, limit);

        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                          "если смещение превышает общее количество элементов")]
    public async Task GetAllAsync_OffsetExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(6, 100);

        TarifficationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => TarifficationService.GetAllAsync(0, offset);

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
        TarifficationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => TarifficationService.GetAllAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать запись по её Id, если она существует в БД")]
    public async Task GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleTariffication = FixtureGenerator.Create<TarifficationRecord>();

        TarifficationRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleTariffication.Id)).ReturnsAsync(sampleTariffication);

        //Act
        var entity = await TarifficationService.GetByIdAsync(sampleTariffication.Id);

        //Assert
        entity.Should().BeEquivalentTo(sampleTariffication.ToGetModel());
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать NotFoundException при вводе отсутствующего в базе Id")]
    public void GetByIdAsync_NonexistentInDbId_ShouldReturnNotFoundException()
    {
        //Act
        var action =  () =>  TarifficationService.GetByIdAsync(Guid.NewGuid());

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync должен возвращать Id добавленной в БД записи")]
    public async Task AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = FixtureGenerator.Create<AddTarifficationModel>();

        TarifficationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<TarifficationRecord>()))
            .ReturnsAsync(Guid.NewGuid());

        //Act
        var resultId = await TarifficationService.AddAsync(model);

        //Assert
        TarifficationRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<TarifficationRecord>(v => v.VerifyBy(model))));

        resultId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Метод UpdateAsync должен возвращать обновляемую в БД модель")]
    public async Task UpdateAsync_ValidInput_UpdateTarifficationModel()
    {
        //Arrange
        var updateModel = FixtureGenerator.Create<UpdateTarifficationModel>();

        TarifficationRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<TarifficationRecord>());

        TarifficationRepositoryMock.Setup(m => m.Update(It.IsAny<TarifficationRecord>()));

        //Act
        var result = await TarifficationService.UpdateAsync(updateModel);

        //Assert
        result.Should().Be(updateModel);
    }

    [Fact(DisplayName = "Метод DeleteAsync должен возвращать булево значение, означающее успешность операции удаления записи")]
    public async Task DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<TarifficationRecord>();

        TarifficationRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        TarifficationRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);

        //Act
        var result = await TarifficationService.DeleteAsync(sampleModel.Id);

        //Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = "Метод Delete должен вернуть NotFoundException при вводе Id несуществующей записи")]
    [AutoData]
    public async Task DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Act
        var action = () => TarifficationService.DeleteAsync(id);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
