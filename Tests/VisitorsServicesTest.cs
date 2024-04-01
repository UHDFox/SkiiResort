using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Visitor;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Visitor;
using FluentAssertions;
using Moq;


namespace Tests;

public sealed class VisitorsServicesTest : TestBase
{
    private IVisitorService VisitorService { get; set; }
    
    public VisitorsServicesTest()
    {
        VisitorService = FixtureGenerator.Create<VisitorService>();
    }
    
    [Theory]
    [AutoData]
    public async void GetListAsync_ValidInput_VisitorRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<VisitorRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<VisitorRecord>());
        }
        
        VisitorsRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await VisitorService.GetListAsync(offset, limit);
        
        //Assert
        Assert.Equal(limit, result.Count);
    }

    [Fact]
    public async void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleVisitor = FixtureGenerator.Create<VisitorRecord>();
        
        VisitorsRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleVisitor.Id)).ReturnsAsync(sampleVisitor);
        
        //Act
        var entity = await VisitorService.GetByIdAsync(sampleVisitor.Id);
        
        //Assert
        entity.Should().BeEquivalentTo(Mapper.Map<GetVisitorModel>(sampleVisitor), 
            x => x.IgnoringCyclicReferences());
    }

    [Fact]
    public async void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var exception = async () => await VisitorService.GetByIdAsync(new Guid());
        
        //Assert
        await Assert.ThrowsAsync<NotFoundException>(exception);
    }

    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnGuid()
    {
        //Arrange
        var controlId = new Guid();

        AddVisitorModel createDto = new AddVisitorModel("capy", 15, "12345678954", "1234-567895");
        VisitorsRepositoryMock.Setup(m => m.AddAsync(Mapper.Map<VisitorRecord>(createDto)))
            .ReturnsAsync(controlId);
        
        //Act
        var resultId = await VisitorService.AddAsync(createDto);
        
        //Assert
        resultId.Should().Be(controlId);
    }

    [Theory]
    [InlineData("1234-1")]
    [InlineData("1234-12345")]
    [InlineData("123-123456")]
    [InlineData("1234 123456")]
    public void AddAsync_InputWithInvalidPassportField_ShouldReturnValidationError(string passport)
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel("capy", 15, "12345678954", passport);
        VisitorsRepositoryMock.Setup(m => m.AddAsync(Mapper.Map<VisitorRecord>(createDto)))
            .Throws(new ValidationException());
        
        //Act
        var exception = () => VisitorService.AddAsync(createDto);

        //Assert
        Assert.ThrowsAsync<ValidationException>(exception);
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890123")]
    [InlineData("1-234-123-47-97")]
    public void AddAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel("capy", 15, phone, "1234-123456");
        VisitorsRepositoryMock.Setup(m => m.AddAsync(Mapper.Map<VisitorRecord>(createDto)))
            .ThrowsAsync(new ValidationException());
        
        //Act
        var exception = () => VisitorService.AddAsync(createDto);

        //Assert
        Assert.ThrowsAsync<ValidationException>(exception);
    }

    [Fact]
    public async void UpdateAsync_ValidInput_UpdateVisitorModel()
    {
        //Arrange
        
        var updateModel = new UpdateVisitorModel(new Guid(), "name", 10, "123456789",
            new DateTime(2000, 1, 1), "1234-123456");
        
        VisitorsRepositoryMock.Setup(m => m.UpdateAsync(Mapper.Map<VisitorRecord>(updateModel)));
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(Mapper.Map<VisitorRecord>(updateModel));
        
        //Act
        var result = await VisitorService.UpdateAsync(Mapper.Map<UpdateVisitorModel>(updateModel));
        
        //Assert
        result.Should().BeEquivalentTo(result);
    }

    [Theory]
    [InlineData("1234-1")]
    [InlineData("1234-12345")]
    [InlineData("123-123456")]
    [InlineData("1234 123456")]
    public void UpdateAsync_InputWithInvalidPassportField_ShouldReturnValidationError(string passport)
    {
        //Arrange
        var updateModel = new UpdateVisitorModel(new Guid(), "name", 10, "123456789",
            new DateTime(2000, 1, 1), passport);
        
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id)).ThrowsAsync(new ValidationException());
        
        //Act
        var exception = () => VisitorService.UpdateAsync(updateModel);
        
        //Assert
        Assert.ThrowsAsync<ValidationException>(exception);
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890123")]
    [InlineData("1-234-123-47-97")]
    public void UpdateAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        var updateModel = new UpdateVisitorModel(new Guid(), "name", 10, phone,
            new DateTime(2000, 1, 1), "1234-123456");
        
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id)).ThrowsAsync(new ValidationException());
        
        //Act
        var exception = () => VisitorService.UpdateAsync(updateModel);

        //Assert
        Assert.ThrowsAsync<ValidationException>(exception);
    }

    [Fact]
    public void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<VisitorRecord>();
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        VisitorsRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(() => true);
        
        //Act
        var result = VisitorService.DeleteAsync(sampleModel.Id).Result;
        
        //Assert
        Assert.True(result);
    }

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new NotFoundException());
        VisitorsRepositoryMock.Setup(m => m.DeleteAsync(id))
            .ReturnsAsync(true);

        //Act
        var exception = () => VisitorService.DeleteAsync(id);

        //Assert
        Assert.ThrowsAsync<ValidationException>(exception);
    }
}