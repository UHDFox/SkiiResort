using System.ComponentModel.DataAnnotations;
using Application.Exceptions;
using Application.Visitor;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Visitor;
using FluentAssertions;
using Moq;

namespace Tests.Visitor;


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
        result.Count.Should().Be(limit);
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
        entity.Should().BeEquivalentTo(sampleVisitor.ToGetModel());
    }

    [Fact]
    public void GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Arrange
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action =  () =>  VisitorService.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = new AddVisitorModel("capy",15, "12345678954", "1234-123456");
        VisitorsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<VisitorRecord>()))
            .ReturnsAsync(Guid.NewGuid());
        
        //Act
        var resultId = await VisitorService.AddAsync(model);
        
        //Assert
        VisitorsRepositoryMock.Verify(x =>
            x.AddAsync(It.Is<VisitorRecord>(v => v.VerifyBy(model))));
        
        resultId.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("1234-1")]
    [InlineData("1234-12345")]
    [InlineData("123-123456")]
    [InlineData("1234 123456")]
    public void AddAsync_InputWithInvalidPassportField_ShouldReturnValidationException(string passport)
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel( "capy", 15, "12345678954", passport);

        //Act
        var action = () => VisitorService.AddAsync(createDto);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890123")]
    [InlineData("1-234-123-47-97")]
    public void AddAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        AddVisitorModel createDto = new AddVisitorModel("capy", 15, phone, "1234-123456");
        
        //Act
        var action = () => VisitorService.AddAsync(createDto);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }

    [Fact]
    public async void UpdateAsync_ValidInput_UpdateVisitorModel()
    {
        //Arrange
        var updateModel = new UpdateVisitorModel(Guid.NewGuid(), "name", 10, "123456789",
            new DateTime(2000, 1, 1), "1234-123456");
        
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<VisitorRecord>());
        VisitorsRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<VisitorRecord>()));
        
        //Act
        var result = await VisitorService.UpdateAsync(updateModel);
        
        //Assert
        result.Should().Be(updateModel);
    }

    [Theory]
    [InlineData("1234-1")]
    [InlineData("1234-12345")]
    [InlineData("123-123456")]
    [InlineData("1234 123456")]
    public void UpdateAsync_InputWithInvalidPassportField_ShouldReturnValidationError(string passport)
    {
        //Arrange
        var updateModel = new UpdateVisitorModel(Guid.NewGuid(), "name", 10, "123456789",
            new DateTime(2000, 1, 1), passport);
        
        //Act
        var action = () => VisitorService.UpdateAsync(updateModel);
        
        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
    
    [Theory]
    [InlineData("12345678")]
    [InlineData("1234567890123")]
    [InlineData("1-234-123-47-97")]
    public void UpdateAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        var updateModel = new UpdateVisitorModel(Guid.NewGuid(), "name", 10, phone,
            new DateTime(2000, 1, 1), "1234-123456");
        
        //Act
        var action = () => VisitorService.UpdateAsync(updateModel);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async void DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
    {
        //Arrange
        var sampleModel = FixtureGenerator.Create<VisitorRecord>();
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(sampleModel.Id)).ReturnsAsync(sampleModel);
        VisitorsRepositoryMock.Setup(m => m.DeleteAsync(sampleModel.Id))
            .ReturnsAsync(true);
        
        //Act
        var result = await VisitorService.DeleteAsync(sampleModel.Id);
        
        //Assert
        result.Should().BeTrue();
    }

    [Theory]
    [AutoData]
    public void DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Arrange
        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());

        //Act
        var action = () => VisitorService.DeleteAsync(id);

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }
}