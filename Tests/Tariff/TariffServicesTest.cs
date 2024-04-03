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
    
    [Theory]
    [AutoData]
    public async void GetListAsync_ValidInput_TariffRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<TariffRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<TariffRecord>());
        }
        
        TariffsRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await TariffService.GetListAsync(offset, limit);
     
        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleVisitor = FixtureGenerator.Create<TariffRecord>();
        
        TariffsRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleVisitor.Id)).ReturnsAsync(sampleVisitor);
        
        //Act
        var entity = await TariffService.GetByIdAsync(sampleVisitor.Id);
        
        //Assert
        entity.Should().BeEquivalentTo(sampleVisitor.ToGetModel());
    }

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action =  () =>  TariffService.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = new AddTariffModel("capy", 1, true);
        TariffsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<TariffRecord>()))
            .ReturnsAsync(Guid.NewGuid());
        
        //Act
        var resultId = await TariffService.AddAsync(model);
        
        //Assert
        TariffsRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<TariffRecord>(v => v.VerifyBy(model))));
        
        resultId.Should().NotBeEmpty();
    }
    
    [Fact]
    public async void UpdateAsync_ValidInput_UpdateTariffModel()
    {
        //Arrange
        var updateModel = new UpdateTariffModel(Guid.NewGuid(), "name", 1, true);
        
        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<TariffRecord>());
        TariffsRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<TariffRecord>()));
        
        //Act
        var result = await TariffService.UpdateAsync(updateModel);
        
        //Assert
        result.Should().Be(updateModel);
    }

    [Fact]
    public async void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<TariffRecord>();
        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        TariffsRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);
        
        //Act
        var result = await TariffService.DeleteAsync(sampleModel.Id);
        
        //Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action = () => TariffService.DeleteAsync(id);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
}
