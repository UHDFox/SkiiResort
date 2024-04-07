using Application.Exceptions;
using Application.Skipass;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Skipass;
using FluentAssertions;
using Moq;

namespace Tests.Skipass;

public sealed class SkipasservicesTest : TestBase
{
    private ISkipassService Skipasservice { get; set; }

    public SkipasservicesTest()
    {
        Skipasservice = FixtureGenerator.Create<SkipassService>();
    }
    
    [Theory]
    [AutoData]
    public async void GetListAsync_ValidInput_SkipassRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<SkipassRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<SkipassRecord>());
        }
        
        SkipassRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await Skipasservice.GetListAsync(offset, limit);
        
        //Assert
        result.Count.Should().Be(limit);
    }
    
    [Fact]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleSkipass = FixtureGenerator.Create<SkipassRecord>();
        
        SkipassRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleSkipass.Id)).ReturnsAsync(sampleSkipass);
        
        //Act
        var entity = await Skipasservice.GetByIdAsync(sampleSkipass.Id);
        
        //Assert
        entity.Should().BeEquivalentTo(sampleSkipass.ToGetModel());
    }

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action =  () =>  Skipasservice.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = new AddSkipassModel(1000, Guid.NewGuid(), Guid.NewGuid(), true);
        SkipassRepositoryMock.Setup(m => m.AddAsync(It.IsAny<SkipassRecord>()))
            .ReturnsAsync(Guid.NewGuid());
        
        //Act
        var resultId = await Skipasservice.AddAsync(model);
        
        //Assert
        SkipassRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<SkipassRecord>(v => v.VerifyBy(model))));
        
        resultId.Should().NotBeEmpty();
    }

    [Fact]
    public async void UpdateAsync_ValidInput_UpdateSkipassModel()
    {
        //Arrange
        var updateModel = new UpdateSkipassModel(Guid.NewGuid(), 1000, Guid.NewGuid(),
            Guid.NewGuid(), true);
        
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<SkipassRecord>());
        SkipassRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<SkipassRecord>()));
        
        //Act
        var result = await Skipasservice.UpdateAsync(updateModel);
        
        //Assert
        result.Should().Be(updateModel);
    }
    
    [Fact]
    public async void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<SkipassRecord>();
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        SkipassRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);
        
        //Act
        var result = await Skipasservice.DeleteAsync(sampleModel.Id);
        
        //Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action = () => Skipasservice.DeleteAsync(id);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
}