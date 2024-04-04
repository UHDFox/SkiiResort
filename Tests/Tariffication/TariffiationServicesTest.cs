using Application.Exceptions;
using Application.Tariffication;
using Application.Tariffication.Models;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Tariffication;
using FluentAssertions;
using Moq;

namespace Tests.Tariffication;

public sealed class TariffiationServicesTest : TestBase
{
    private ITarifficationService TarifficationService { get; set; }

    public TariffiationServicesTest()
    {
        TarifficationService = FixtureGenerator.Create<TarifficationService>();
    }
    
     [Theory]
    [AutoData]
    public async void GetListAsync_ValidInput_TarifficationRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<TarifficationRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<TarifficationRecord>());
        }
        
        TarifficationRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await TarifficationService.GetAllAsync(offset, limit);
        
        //Assert
        result.Count.Should().Be(limit);
    }

    [Fact]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
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

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        TarifficationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action =  () =>  TarifficationService.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = new AddTarifficationModel(200, Guid.NewGuid(), Guid.NewGuid());
        TarifficationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<TarifficationRecord>()))
            .ReturnsAsync(Guid.NewGuid());
        
        //Act
        var resultId = await TarifficationService.AddAsync(model);
        
        //Assert
        TarifficationRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<TarifficationRecord>(v => v.VerifyBy(model))));
        
        resultId.Should().NotBeEmpty();
    }

    [Fact]
    public async void UpdateAsync_ValidInput_UpdateTarifficationModel()
    {
        //Arrange
        var updateModel = new UpdateTarifficationModel(Guid.NewGuid(), 200, Guid.NewGuid(), Guid.NewGuid());
        
        TarifficationRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<TarifficationRecord>());
        TarifficationRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<TarifficationRecord>()));
        
        //Act
        var result = await TarifficationService.UpdateAsync(updateModel);
        
        //Assert
        result.Should().Be(updateModel);
    }

    [Fact]
    public async void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
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

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        TarifficationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action = () => TarifficationService.DeleteAsync(id);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
}