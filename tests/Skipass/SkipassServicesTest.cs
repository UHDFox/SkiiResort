using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Skipass;
using SkiiResort.Domain.Entities.Skipass;

namespace SkiiResort.Tests.Skipass;

public sealed class SkipassServicesTest : TestBase
{
    private ISkipassService SkipassService { get; set; }

    public SkipassServicesTest()
    {
        SkipassService = FixtureGenerator.Create<SkipassService>();
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет список ски-пассов c определенного индекса и указанной длины")]
    public async Task GetAllAsync_ValidInput_ReturnsListOfSkipassesFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<SkipassRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<SkipassRecord>());
        }

        SkipassRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        SkipassRepositoryMock.Setup(m => m.GetAllAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await SkipassService.GetListAsync(offset, limit);

        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                          "если смещение превышает общее количество элементов")]
    public async Task GetAllAsync_OffsetExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(6, 100);

        SkipassRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => SkipassService.GetListAsync(0, offset);

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
        SkipassRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => SkipassService.GetListAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать запись по её Id, если она существует в БД")]
    public async Task GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleSkipass = FixtureGenerator.Create<SkipassRecord>();

        SkipassRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleSkipass.Id)).ReturnsAsync(sampleSkipass);

        //Act
        var entity = await SkipassService.GetByIdAsync(sampleSkipass.Id);

        //Assert
        entity.Should().BeEquivalentTo(sampleSkipass.ToGetModel());
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать NotFoundException при вводе отсутствующего в базе Id")]
    public void GetByIdAsync_NonexistentInDbId_ShouldReturnNotFoundException()
    {
        //Act
        var action =  () =>  SkipassService.GetByIdAsync(Guid.NewGuid());

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync должен возвращать Id добавленной в БД записи")]
    public async Task AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = FixtureGenerator
            .Build<AddSkipassModel>()
            .With(f => f.Status, true)
            .Create();

        SkipassRepositoryMock.Setup(m => m.AddAsync(It.IsAny<SkipassRecord>()))
            .ReturnsAsync(Guid.NewGuid());

        //Act
        var resultId = await SkipassService.AddAsync(model);

        //Assert
        SkipassRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<SkipassRecord>(v => v.VerifyBy(model))));

        resultId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Метод UpdateAsync должен возвращать обновляемую в БД модель")]
    public async Task UpdateAsync_ValidInput_UpdateSkipassModel()
    {
        //Arrange
        var updateModel = FixtureGenerator
            .Build<UpdateSkipassModel>()
            .With(m => m.Status, true)
            .Create();

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<SkipassRecord>());

        SkipassRepositoryMock.Setup(m => m.Update(It.IsAny<SkipassRecord>()));

        //Act
        var result = await SkipassService.UpdateAsync(updateModel);

        //Assert
        result.Should().Be(updateModel);
    }

    [Fact(DisplayName = "Метод DeleteAsync должен возвращать булево значение, означающее успешность операции удаления записи")]
    public async Task DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<SkipassRecord>();

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        SkipassRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);

        //Act
        var result = await SkipassService.DeleteAsync(sampleModel.Id);

        //Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = "Метод Delete должен вернуть NotFoundException при вводе Id несуществующей записи")]
    [AutoData]
    public async Task DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Act
        var action = () => SkipassService.DeleteAsync(id);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
