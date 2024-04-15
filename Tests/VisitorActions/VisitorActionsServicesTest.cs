using AutoFixture;
using FluentAssertions;
using Moq;
using SkiiResort.Application.Exceptions;
using SkiiResort.Application.VisitorAction;
using SkiiResort.Domain.Entities.Location;
using SkiiResort.Domain.Entities.Skipass;
using SkiiResort.Domain.Entities.Tariff;
using SkiiResort.Domain.Entities.Tariffication;
using SkiiResort.Domain.Entities.VisitorsAction;
using SkiiResort.Domain.Enums;

namespace SkiiResort.Tests.VisitorActions;

public sealed class VisitorActionsServicesTest : TestBase
{
    private IVisitorActions VisitorActionsService { get; set; }

    public VisitorActionsServicesTest()
    {
        VisitorActionsService = FixtureGenerator.Create<VisitorActionsService>();
    }


    [Fact(DisplayName = "Метод GetAllAsync вернет список действий посетителя c определенного индекса и указанной длины")]

    public async Task GetAllAsync_ValidInput_ReturnsListOfVisitorActionsFromInSomeRange()
    {
        //Arrange
        var limit = new Random().Next(0, 20);
        var offset = new Random().Next(0, 10000);
        var sampleList = new List<VisitorActionsRecord>();

        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<VisitorActionsRecord>());
        }

        VisitorActionsRepositoryMock.Setup(m => m.GetTotalAmountAsync())
            .ReturnsAsync(new Random().Next(limit + offset, int.MaxValue));

        VisitorActionsRepositoryMock.Setup(m => m.GetAllAsync(offset, limit))
            .ReturnsAsync(sampleList);

        //Act
        var result = await VisitorActionsService.GetAllAsync(offset, limit);

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
        var action = () => VisitorActionsService.GetAllAsync(0, offset);

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
        var action = () => VisitorActionsService.GetAllAsync(offset, limit);

        //Assert
        await action.Should().ThrowAsync<PaginationQueryException>("queried page exceeds total amount of records");
    }

    [Fact]
    public async Task GetByIdAsync_ValidInput_ShouldReturnGetModel()
    {
        //Arrange
        var sampleVisitorActions = FixtureGenerator.Create<VisitorActionsRecord>();

        VisitorActionsRepositoryMock.Setup(method => method
            .GetByIdAsync(sampleVisitorActions.Id)).ReturnsAsync(sampleVisitorActions);

        //Act
        var entity = await VisitorActionsService.GetByIdAsync(sampleVisitorActions.Id);

        //Assert
        entity.Should().BeEquivalentTo(sampleVisitorActions);
    }

    [Fact]
    public async Task GetByIdAsync_NoneInput_ShouldReturnNotFoundException()
    {
        //Act
        var action = () =>  VisitorActionsService.GetByIdAsync(Guid.NewGuid());

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }


    [Fact(DisplayName = "Метод AddAsync добавляет новую запись Действий посетителя " +
                        "и обновляет баланс Скайпасса, если тот не VIP")]
    public async Task AddAsync_ValidInput_ReturnsModelIdAndUpdatesSkipassBalance()
    {
        //Arrange
        var skipassEntity = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, true)
            .Create();

        var visitorActionModel = FixtureGenerator
            .Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.TransactionType, (OperationType)new Random().Next(0, 1))
            .Create();

        var locationEntity = FixtureGenerator
            .Build<LocationRecord>()
            .With(m => m.Id, visitorActionModel.LocationId)
            .Create();

        var tariffEntity = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.Id, skipassEntity.TariffId)
            .With(m => m.IsVip, false)
            .With(m => m.Tariffications, new []
                {new TarifficationRecord(skipassEntity.Balance - 1, skipassEntity.TariffId, locationEntity.Id)})
            .Create();


        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(skipassEntity);

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(tariffEntity.Id))
            .ReturnsAsync(tariffEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(locationEntity);

        TarifficationRepositoryMock.Setup(m => m.GetByLocationAndTariffIdsAsync(tariffEntity.Id, locationEntity.Id))
            .ReturnsAsync(tariffEntity.Tariffications[0]);

        VisitorActionsRepositoryMock.Setup(m => m.AddAsync(It.IsAny<VisitorActionsRecord>()))
            .ReturnsAsync(Guid.NewGuid());

        //Act
        await VisitorActionsService.AddAsync(visitorActionModel);

        //Assert
        SkipassRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(skipassEntity.Id))));
        VisitorActionsRepositoryMock.Setup(m => m.AddAsync(It.Is<VisitorActionsRecord>(
            v => v.VerifyBy(visitorActionModel.ToEntity()))));
        LocationRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(locationEntity.Id))));
        ((int)visitorActionModel.TransactionType!).Should().BeInRange(0, 1);
    }

    [Fact(DisplayName = "Метод AddAsync вернет ArgumentOutOfRangeException, если тип операции действия посетителя невалиден ")]
    public async Task AddAsync_SkipassWithInvalidTransactionType_ThrowsArgumentOutOfRangeException()
    {
        //Arrange
        var skipassEntity = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Balance, 1)
            .With(m => m.Status, true)
            .Create();

        var visitorActionModel = FixtureGenerator
            .Build<VisitorActionsRecord>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.TransactionType, (OperationType)new Random().Next (2, 100))
            .Create();

        var locationEntity = FixtureGenerator
            .Build<LocationRecord>()
            .With(m => m.Id, visitorActionModel.LocationId)
            .Create();

        var tariffEntity = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.Id, skipassEntity.TariffId)
            .With(m => m.Tariffications, new []
                {new TarifficationRecord(100, skipassEntity.TariffId, locationEntity.Id)})
            .Create();


        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(skipassEntity);

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(tariffEntity.Id))
            .ReturnsAsync(tariffEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(locationEntity);

        TarifficationRepositoryMock.Setup(m => m.GetByLocationAndTariffIdsAsync(tariffEntity.Id, locationEntity.Id))
            .ReturnsAsync(tariffEntity.Tariffications[0]);

        VisitorActionsRepositoryMock.Setup(m => m.AddAsync(visitorActionModel));

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionModel.ToAddModel());

        //Assert
        await action.Should().ThrowAsync<ArgumentOutOfRangeException>();

        SkipassRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(skipassEntity.Id))));
        (skipassEntity.Balance - tariffEntity.Tariffications[0].Price).Should().BeNegative();
        LocationRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(locationEntity.Id))));
        ((int)visitorActionModel.TransactionType).Should().NotBeInRange(0, 1);
    }

    [Fact(DisplayName = "Метод AddAsync вернет SkipassStatusException, если снимаемая сумма превышает баланс скайпасса")]
    public async Task AddAsync_SkipassWithNotEnoughBalance_ThrowsSkipassStatusException()
    {
        //Arrange
        var skipassEntity = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Balance, 1)
            .With(m => m.Status, true)
            .Create();

        var visitorActionModel = FixtureGenerator
            .Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.TransactionType, OperationType.Negative)
            .Create();


        var locationEntity = FixtureGenerator
            .Build<LocationRecord>()
            .With(m => m.Id, visitorActionModel.LocationId)
            .Create();

        var tariffEntity = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.Id, skipassEntity.TariffId)
            .With(m => m.IsVip, false)
            .With(m => m.Tariffications,
                new [] {new TarifficationRecord(1000, skipassEntity.TariffId, locationEntity.Id)})
            .Create();


        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(skipassEntity);

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(tariffEntity.Id))
            .ReturnsAsync(tariffEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(locationEntity);

        TarifficationRepositoryMock.Setup(m => m.GetByLocationAndTariffIdsAsync(tariffEntity.Id, locationEntity.Id))
            .ReturnsAsync(tariffEntity.Tariffications[0]);

        VisitorActionsRepositoryMock.Setup(m => m.AddAsync(visitorActionModel.ToEntity()));

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionModel);

        //Assert
        await action.Should().ThrowAsync<SkipassStatusException>();

        SkipassRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(skipassEntity.Id))));

        (skipassEntity.Balance - tariffEntity.Tariffications[0].Price).Should().BeNegative();

        LocationRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v => v.Equals(locationEntity.Id))));
    }

    [Fact(DisplayName = "Метод AddAsync возвращает NotFoundException, если модель ссылается на Id несуществующего скайпасса")]
    public async Task AddAsync_ModelWithNonexistentSkipassId_ThrowsNotFoundException()
    {
        var visitorActionsModelModel = FixtureGenerator.Create<AddVisitorActionsModel>();

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsModelModel);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод AddAsync возвращает SkipassStatusException, если у модель ссылается на неактивный скайпасс")]
    public async Task AddAsync_ModelWithInactiveSkipass_ThrowsSkipassStatusException()
    {
        //Arrange

        var sampleSkipassRecord = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, false)
            .Create();

        var visitorActionsRecord = FixtureGenerator
            .Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, sampleSkipassRecord.Id)
            .Create();

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);

        //Assert
        await action.Should().ThrowAsync<SkipassStatusException>();

        SkipassRepositoryMock.Verify(m =>
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.SkipassId))));

    }

    [Fact(DisplayName = "Метод AddAsync вернет NotFoundException, если относящийся к модели скайпасс ссылается на несуществующий тарифф")]
    public async Task AddAsync_RelatedToVisitorActionSkipassHasInvalidTariffId_ThrowsNotFoundException()
    {
        //Arrange
        var sampleSkipassRecord = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, true)
            .Create();

        var visitorActionsRecord =
            FixtureGenerator
                .Build<AddVisitorActionsModel>()
                .With(m => m.SkipassId, sampleSkipassRecord.Id)
                .Create();

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();

        TariffRepositoryMock.Verify(m =>
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(sampleSkipassRecord.TariffId))));
    }

    [Fact(DisplayName = ("Метод AddAsync вернет NotFoundException, если запись действия посетителя ссылается на несуществующую запись места"))]
    public async Task AddAsync_RelatedToVisitorActionSkipassHasInvalidLocationId_ThrowsNotFoundException()
    {
        //Arrange
        var sampleSkipassRecord = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, true)
            .Create();

        var sampleLocationRecord = FixtureGenerator.Create<LocationRecord>();

        var sampleTariffRecord = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.IsVip, false)
            .Create();
        sampleTariffRecord.Tariffications.Append(new TarifficationRecord(100, sampleTariffRecord.Id, sampleLocationRecord.Id));


        var visitorActionsRecord = new AddVisitorActionsModel(sampleSkipassRecord.Id, sampleLocationRecord.Id);
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);
        TariffRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(sampleTariffRecord);


        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>("current tariff doesn't set the price for that location");

        LocationRepositoryMock.Verify(m =>
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.LocationId))));
    }

    [Fact(DisplayName = "Метод AddAsync вернет NotFoundException, если не удается найти тарификацию по айди скайпасса и тарифа")]
    public async Task AddAsync_ModelWithRelatedTariffAndSkipassThatDontShareATarrification_ThrowsNotFoundException()
    {
        //Arrange
        var skipassEntity = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, true)
            .Create();

        var tariffEntity = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.IsVip, false)
            .Create();

        var locationEntity = FixtureGenerator.Create<LocationRecord>();

        tariffEntity.Tariffications.Append(new TarifficationRecord(100, tariffEntity.Id, locationEntity.Id));

        var model = FixtureGenerator.Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.LocationId, locationEntity.Id)
            .Create();

        FixtureGenerator
            .Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.LocationId, locationEntity.Id)
            .Create();

        VisitorActionsRepositoryMock.Setup(m =>
            m.AddAsync(It.IsAny<VisitorActionsRecord>())).ReturnsAsync(Guid.NewGuid());

        VisitorActionsRepositoryMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

        SkipassRepositoryMock.Setup(m =>
            m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(skipassEntity);

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(tariffEntity);

        //Act
        var action = () => VisitorActionsService.AddAsync(model);

        //Assert
        await action.Should()
            .ThrowAsync<NotFoundException>("couldn't find tariffication related to such tariff and location");
    }

    [Fact(DisplayName = "Метод UpdateAsync обновляет запись скайпасса и действия посетителя, возвращает запись последнего")]
    public async Task UpdateASync_ValidInput_UpdatesVisitorActionAndSkipassEntities()
    {
        //Arrange
        var tariffEntity = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.IsVip, true)
            .Create();

        var skipassEntity = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.TariffId, tariffEntity.Id)
            .With(m => m.Status, true)
            .Create();

        var locationEntity = FixtureGenerator
            .Build<LocationRecord>()
            .With(m => m.Id, Guid.NewGuid)
            .Create();

        var updateVisitorsActionModel = FixtureGenerator
            .Build<UpdateVisitorActionsModel>()
            .With(m => m.SkipassId, skipassEntity.Id)
            .With(m => m.LocationId, locationEntity.Id)
            .Create();

        var tarifficationEntity = FixtureGenerator
            .Build<TarifficationRecord>()
            .With(m => m.TariffId, tariffEntity.Id)
            .With(m => m.LocationId, locationEntity.Id)
            .Create();

        VisitorActionsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(updateVisitorsActionModel.ToEntity);

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(skipassEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(updateVisitorsActionModel.LocationId))
            .ReturnsAsync(locationEntity);

        TarifficationRepositoryMock.Setup(m => m.GetByLocationAndTariffIdsAsync(tariffEntity.Id, locationEntity.Id))
            .ReturnsAsync(tarifficationEntity);

        SkipassRepositoryMock.Setup(m => m.UpdateAsync(skipassEntity));

        //Act
        var result = await VisitorActionsService.UpdateAsync(updateVisitorsActionModel);

        //Assert
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Метод UpdateAsync вернет NotFoundException, если попытаться обновить несуществующую запись")]
    public async Task UpdateAsync_RequestWithInvalidVisitorsActionId_NotFoundException()
    {
        //Arrange
        var updateModel = FixtureGenerator.Create<UpdateVisitorActionsModel>();

        //Act
        var action = () => VisitorActionsService.UpdateAsync(updateModel);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>("Visitors action not found");

        VisitorActionsRepositoryMock.Verify(m => m.GetByIdAsync(It.Is<Guid>(v =>
            v.Equals(updateModel.Id))));
    }

    [Fact(DisplayName = "Метод UpdateAsync вернет NotFoundException, если не будет найден скайпасс по айди")]
    public async Task UpdateAsync_ModelWithInvalidSkipassId_ThrowsNotFoundException()
    {
        //Arrange
        var visitorActionsModelModel = FixtureGenerator.Create<AddVisitorActionsModel>();

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsModelModel);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact(DisplayName = "Метод UpdateAsync возвращает SkipassStatusException, если у модель ссылается на неактивный скайпасс")]
    public async Task UpdateAsync_ModelWithInactiveSkipass_ThrowsSkipassStatusException()
    {
        //Arrange

        var sampleSkipassRecord = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, false)
            .Create();

        var visitorActionsRecord = FixtureGenerator
            .Build<AddVisitorActionsModel>()
            .With(m => m.SkipassId, sampleSkipassRecord.Id)
            .Create();

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);

        //Assert
        await action.Should().ThrowAsync<SkipassStatusException>();

        SkipassRepositoryMock.Verify(m =>
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.SkipassId))));
    }

    [Fact(DisplayName = ("Метод UpdateAsync вернет NotFoundException, если запись действия посетителя ссылается на несуществующую запись места"))]
    public async Task UpdateAsync_RelatedToVisitorActionSkipassHasInvalidLocationId_ThrowsNotFoundException()
    {
        //Arrange
        var sampleSkipassRecord = FixtureGenerator
            .Build<SkipassRecord>()
            .With(m => m.Status, true)
            .Create();

        var sampleLocationRecord = FixtureGenerator.Create<LocationRecord>();

        var sampleTariffRecord = FixtureGenerator
            .Build<TariffRecord>()
            .With(m => m.IsVip, false)
            .Create();

        sampleTariffRecord.Tariffications.Append(new TarifficationRecord(100, sampleTariffRecord.Id, sampleLocationRecord.Id));

        var visitorActionsRecord = new AddVisitorActionsModel(sampleSkipassRecord.Id, sampleLocationRecord.Id);
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        TariffRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(sampleTariffRecord);

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);

        //Assert
        await action.Should().ThrowAsync<NotFoundException>("current tariff doesn't set the price for that location");

        LocationRepositoryMock.Verify(m =>
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.LocationId))));
    }
}
