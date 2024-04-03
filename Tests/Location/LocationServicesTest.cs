using Application.Exceptions;
using Application.Location;
using Application.Location.Models;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Location;
using FluentAssertions;
using Moq;


namespace Tests.Location;

public sealed class LocationServicesTest : TestBase
{
    private ILocationService LocationService { get; set; }
    
    public LocationServicesTest()
    {
        LocationService = FixtureGenerator.Create<LocationService>();
    }
    
    [Theory]
    [AutoData]
    public async void GetListAsync_ValidInput_LocationRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<LocationRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<LocationRecord>());
        }
        
        LocationRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await LocationService.GetAllAsync(offset, limit);
     
        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleVisitor = FixtureGenerator.Create<LocationRecord>();
        
        LocationRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleVisitor.Id)).ReturnsAsync(sampleVisitor);
        
        //Act
        var entity = await LocationService.GetByIdAsync(sampleVisitor.Id);
        
        //Assert
        entity.Should().BeEquivalentTo(sampleVisitor.ToGetModel());
    }

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action =  () =>  LocationService.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = new AddLocationModel("capy");
        LocationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<LocationRecord>()))
            .ReturnsAsync(Guid.NewGuid());
        
        //Act
        var resultId = await LocationService.AddAsync(model);
        
        //Assert
        LocationRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<LocationRecord>(v => v.VerifyBy(model))));
        
        resultId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async void UpdateAsync_ValidInput_UpdateLocationModel()
    {
        //Arrange
        var updateModel = new UpdateLocationModel(Guid.NewGuid(), "name");
        
        LocationRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<LocationRecord>());
        LocationRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<LocationRecord>()));
        
        //Act
        var result = await LocationService.UpdateAsync(updateModel);
        
        //Assert
        result.Should().Be(updateModel);
    }

    [Fact]
    public async void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
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

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action = () => LocationService.DeleteAsync(id);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
}