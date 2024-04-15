using System.ComponentModel.DataAnnotations;
using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Moq;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.Visitor;
using SkiiResort.Domain.Entities.Visitor;

namespace SkiiResort.Tests.Visitor;

public sealed class VisitorServicesTest : TestBase
{
    private IVisitorService VisitorService { get; set; }

    public VisitorServicesTest()
    {
        VisitorService = FixtureGenerator.Create<VisitorService>();
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет список посетителей c определенного индекса и указанной длины")]
    public async Task GetAllAsync_ValidInput_ReturnsListOfVisitorActionsFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<VisitorRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<VisitorRecord>());
        }

        VisitorsRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        VisitorsRepositoryMock.Setup(m => m.GetAllAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await VisitorService.GetListAsync(offset, limit);

        //Assert

        result.Count.Should().Be(limit);
    }

    [Fact(DisplayName = "Метод GetAllAsync вернет PaginationQueryException, " +
                          "если смещение превышает общее количество элементов")]
    public async Task GetAllAsync_OffsetExceedsTotalAmountOfRecords_ThrowsPaginationQueryException()
    {
        //Arrange
        var offset = new Random().Next(6, 100);

        VisitorActionsRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => VisitorService.GetListAsync(0, offset);

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
        VisitorActionsRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(1, 5));

        //Act
        var action = () => VisitorService.GetListAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать запись по её Id, если она существует в БД")]
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

    [Fact(DisplayName = "Метод GetByIdAsync должен возвращать NotFoundException при вводе отсутствующего в базе Id")]
    public void GetByIdAsync_NonexistentInDbId_ShouldReturnNotFoundException()
    {
        //Act
        var action =  () =>  VisitorService.GetByIdAsync(Guid.NewGuid());

        //Assert
        action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync должен возвращать Id добавленной в БД записи")]
    public async Task AddAsync_ValidRequest_ShouldReturnEntityId()
    {
        //Arrange
        var model = FixtureGenerator
            .Build<AddVisitorModel>()
            .With(m => m.Phone, "123456789")
            .With(m => m.Passport, "1234-123456")
            .Create();

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
    [AutoData]
    public void AddAsync_InputWithInvalidPassportField_ShouldReturnValidationException(string passport)
    {
        //Arrange
        var createDto = FixtureGenerator.Build<AddVisitorModel>()
            .With(m => m.Phone, "123456789")
            .With(m => m.Passport, passport)
            .Create();

        //Act
        var action = () => VisitorService.AddAsync(createDto);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }

    [Theory]
    [AutoData]
    public void AddAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        var createDto = FixtureGenerator.Build<AddVisitorModel>()
            .With(m => m.Phone, phone)
            .With(m => m.Passport, "1234-123456")
            .Create();

        //Act
        var action = () => VisitorService.AddAsync(createDto);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Метод UpdateAsync должен возвращать обновляемую в БД модель")]
    public async Task UpdateAsync_ValidInput_UpdateVisitorModel()
    {
        //Arrange
        var updateModel = FixtureGenerator
            .Build<UpdateVisitorModel>()
            .With(m => m.Phone, "123456789")
            .With(m => m.Passport, "1234-123456")
            .Create();

        VisitorsRepositoryMock.Setup(m => m.GetByIdAsync(updateModel.Id))
            .ReturnsAsync(FixtureGenerator.Create<VisitorRecord>());

        VisitorsRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<VisitorRecord>()));

        //Act
        var result = await VisitorService.UpdateAsync(updateModel);

        //Assert
        result.Should().Be(updateModel);
    }

    [Theory(DisplayName = "Метод UpdateAsync должен вернуть ValidationException для моделей с некорректным паспортом")]
    [AutoData]
    public void UpdateAsync_InputWithInvalidPassportField_ShouldReturnValidationError(string passport)
    {
        //Arrange
        var updateModel = FixtureGenerator.Build<UpdateVisitorModel>()
            .With(m => m.Phone, "123456789")
            .With(m => m.Passport, passport)
            .Create();

        //Act
        var action = () => VisitorService.UpdateAsync(updateModel);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }

    [Theory(DisplayName = "Метод UpdateAsync должен вернуть ValidationException для моделей с некорректным номером телефона")]
    [AutoData]
    public void UpdateAsync_InputWithInvalidPhoneNumberField_ShouldReturnValidationError(string phone)
    {
        //Arrange
        var updateModel = FixtureGenerator.Build<UpdateVisitorModel>()
            .With(m => m.Passport, "1234-123456")
            .With(m => m.Phone, phone)
            .Create();

        //Act
        var action = () => VisitorService.UpdateAsync(updateModel);

        //Assert
        action.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Метод DeleteAsync должен возвращать булево значение, означающее успешность операции удаления записи")]
    public async Task DeleteAsync_ValidInput_ShouldReturnBooleanTrue()
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

    [Theory(DisplayName = "Метод Delete должен вернуть NotFoundException при вводе Id несуществующей записи")]
    [AutoData]
    public async Task DeleteAsync_InvalidInput_ShouldThrowNotFoundException(Guid id)
    {
        //Act
        var action = () => VisitorService.DeleteAsync(id);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }
}
