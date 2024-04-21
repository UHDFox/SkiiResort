using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Location;
using SkiiResort.Application.Location.Models;
using SkiiResort.Domain.Entities.Location;

namespace SkiiResort.Tests.Location;

public sealed class LocationServicesTest : TestBase
{
    private ILocationService LocationService { get; set; }

    public LocationServicesTest()
    {
        LocationService = FixtureGenerator.Create<LocationService>();
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет список локаций c определенного индекса и указанной длины")]
    public async Task GetAllAsync_ValidInput_ReturnsListOfLocationsFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<LocationRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<LocationRecord>());
        }

        LocationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        LocationRepositoryMock.Setup(m => m.GetAllAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await LocationService.GetAllAsync(offset, limit);

        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                          "если смещение превышает общее количество элементов")]
    public async Task GetAllAsync_OffsetExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(6, 100);

        LocationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => LocationService.GetAllAsync(0, offset);

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
        LocationRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => LocationService.GetAllAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать запись по её Id, если она существует в БД")]
    public async Task GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleLocation = FixtureGenerator.Create<LocationRecord>();

        LocationRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleLocation.Id)).ReturnsAsync(sampleLocation);

        //Act
        var entity = await LocationService.GetByIdAsync(sampleLocation.Id);

        //Assert
        entity.Should().BeEquivalentTo(sampleLocation.ToGetModel());
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать NotFoundException при вводе отсутствующего в базе Id")]
    public void GetByIdAsync_NonexistentInDbId_ShouldReturnNotFoundException()
    {
        //Act
        var action =  () =>  LocationService.GetByIdAsync(Guid.NewGuid());

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync должен возвращать Id добавленной в БД записи")]
    public async Task AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = FixtureGenerator.Create<AddLocationModel>();

        LocationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<LocationRecord>()))
            .ReturnsAsync(Guid.NewGuid());

        //Act
        var resultId = await LocationService.AddAsync(model);

        //Assert
        LocationRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<LocationRecord>(v => v.VerifyBy(model))));

        resultId.Should().NotBeEmpty();
    }

    [Fact(DisplayName = "Метод UpdateAsync должен возвращать обновляемую в БД модель")]
    public async Task UpdateAsync_ValidInput_UpdateLocationModel()
    {
        //Arrange
        var updateModel = FixtureGenerator.Create<UpdateLocationModel>();

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<LocationRecord>());

        LocationRepositoryMock.Setup(m => m.Update(It.IsAny<LocationRecord>()));

        //Act
        var result = await LocationService.UpdateAsync(updateModel);

        //Assert
        result.Should().Be(updateModel);
    }

    [Fact(DisplayName = "Метод DeleteAsync должен возвращать булево значение, означающее успешность операции удаления записи")]
    public async Task DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<LocationRecord>();

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        LocationRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);

        //Act
        var result = await LocationService.DeleteAsync(sampleModel.Id);

        //Assert
        result.Should().BeTrue();
    }

    [Theory(DisplayName = "Метод Delete должен вернуть NotFoundException при вводе Id несуществующей записи")]
    [AutoData]
    public async Task DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Act
        var action = () => LocationService.DeleteAsync(id);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
