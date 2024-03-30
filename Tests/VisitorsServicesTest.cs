using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Visitor;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Visitor;
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
    public void GetListAsync_ValidInput_VisitorRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<VisitorRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<VisitorRecord>());
        }
        
        VisitorsRepositoryMock.Setup(method => method
            .GetListAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(sampleList);
        
        //Act
        var result = VisitorService.GetListAsync(offset, limit).Result;
        
        //Assert
        Assert.Equal(limit, result.Count);
    }

    [Fact]
    public void GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleVisitor = FixtureGenerator.Create<VisitorRecord>();
        VisitorsRepositoryMock.Setup(method => method
            .GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(sampleVisitor);
        
        //Act
        var entity = VisitorService.GetByIdAsync(new Guid()).Result;
        
        //Assert
        Assert.IsType<GetVisitorModel>(entity);
    }

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var exception = Assert.ThrowsAsync<NotFoundException>(async () => await VisitorService.GetByIdAsync(new Guid())).Result;
        
        //Assert
        Assert.IsType<NotFoundException>(exception);
    }

    [Fact]
    public void AddAsync_ValidRequest_ShouldReturnGuid()
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel("capy", 15, "12345678954", "1234-567895");
        VisitorsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<VisitorRecord>())).ReturnsAsync(new Guid());
        
        //Act
        var resultId = VisitorService.AddAsync(createDto).Result;
        
        //Assert
        Assert.IsType<Guid>(resultId);
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
        VisitorsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<VisitorRecord>()))
            .Throws(new ValidationException());
        
        //Act
        var exception = VisitorService.AddAsync(createDto).Exception!.InnerException;

        //Assert
        Assert.IsType<ValidationException>(exception);
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890123")]
    [InlineData("1-234-123-47-97")]
    public void AddAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel("capy", 15, phone, "1234-123456");
        VisitorsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<VisitorRecord>()))
            .Throws(new ValidationException());
        
        //Act
        var exception = VisitorService.AddAsync(createDto).Exception!.InnerException;

        //Assert
        Assert.IsType<ValidationException>(exception);
    }

    [Fact]
    public void UpdateAsync_ValidInput_UpdateVisitorModel()
    {
        //Arrange
        
        var updateModel = new UpdateVisitorModel(new Guid(), "name", 10, "123456789",
            new DateTime(2000, 1, 1), "1234-123456");
        var sampleModel = FixtureGenerator.Create<VisitorRecord>();
        
        VisitorsRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<VisitorRecord>()));
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(sampleModel);
        
        //Act
        var result = VisitorService.UpdateAsync(updateModel).Result;
        
        //Assert
        Assert.IsType<UpdateVisitorModel>(result);
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
        
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new ValidationException());
        
        //Act
        var exception = VisitorService.UpdateAsync(updateModel).Exception!.InnerException;
        
        //Assert
        Assert.IsType<ValidationException>(exception);
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
        
        
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new ValidationException());
        
        //Act
        var exception = VisitorService.UpdateAsync(updateModel).Exception!.InnerException;

        //Assert
        Assert.IsType<ValidationException>(exception);
    }

    [Fact]
    public void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<VisitorRecord>();
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(sampleModel);
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
            .ReturnsAsync(() => true);
        
        //Act
        var exception = VisitorService.DeleteAsync(id).Exception!.InnerException;
        
        //Assert
        Assert.IsType<NotFoundException>(exception);
    }
   
    
}