using Application.Exceptions;
using Application.Skipass;
using Application.VisitorAction;
using AutoFixture;
using AutoFixture.Xunit2;
using Domain.Entities.Location;
using Domain.Entities.Skipass;
using Domain.Entities.Tariff;
using Domain.Entities.Tariffication;
using Domain.Entities.VisitorsAction;
using FluentAssertions;
using Moq;
using Tests.Skipass;

namespace Tests.VisitorActions;

public sealed class VisitorActionsServicesTest : TestBase
{
    private IVisitorActions VisitorActionsService { get; set; }

    public VisitorActionsServicesTest()
    {
        VisitorActionsService = FixtureGenerator.Create<VisitorActionsService>();
    }
    
    
    [Theory]
    [AutoData]
    public async Task GetListAsync_ValidInput_VisitorActionsRecordList(int offset, int limit)
    {
        //Arrange

        var sampleList = new List<VisitorActionsRecord>();
        while (sampleList.Count < limit)
        {
            sampleList.Add(FixtureGenerator.Create<VisitorActionsRecord>());
        }
        
        VisitorActionsRepositoryMock.Setup(method => method
            .GetListAsync(offset, limit)).ReturnsAsync(sampleList);
        
        //Act
        var result = await VisitorActionsService.GetAllAsync(offset, limit);
     
        //Assert

        result.Count.Should().Be(limit);
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
        //Arrange
        VisitorActionsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException());
        
        //Act
        var action = () =>  VisitorActionsService.GetByIdAsync(Guid.NewGuid());
        
        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task AddAsync_ValidInput_ReturnsModelIdAndUpdatesSkipassBalance()
    {
        //Arrange
        var skipassEntity = new SkipassRecord(1000, Guid.NewGuid(), Guid.NewGuid(), true);
        var tariffEntity = new TariffRecord(FixtureGenerator.Create<string>(), 1, false);
        var locationEntity = FixtureGenerator.Create<LocationRecord>();
        tariffEntity.Tariffications.Append(new TarifficationRecord(100, tariffEntity.Id, locationEntity.Id));
        
        VisitorActionsRepositoryMock.Setup(m =>
            m.AddAsync(It.IsAny<VisitorActionsRecord>())).ReturnsAsync(Guid.NewGuid());
        
        VisitorActionsRepositoryMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

        SkipassRepositoryMock.Setup(m =>
            m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(skipassEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(locationEntity);

        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(tariffEntity);
        
        var model = FixtureGenerator.Create<AddVisitorActionsModel>();
        //Act
        var result = await VisitorActionsService.AddAsync(model);
        
        //Assert
        SkipassRepositoryMock.Verify(m => 
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(model.SkipassId))));
        
        SkipassRepositoryMock.Verify(m => 
            m.UpdateAsync(It.Is<SkipassRecord>(v => v.VerifyBy(skipassEntity))));
    }

    [Fact]
    public async Task AddAsync_ModelWithInvalidSkipassId_ThrowsNotFoundException()
    {
        //Arrange
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("Skipass not found"));

        var visitorActionsModelModel = FixtureGenerator.Create<AddVisitorActionsModel>();
        
        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsModelModel);
        
        //Assert
        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task AddAsync_ModelWithInactiveSkipass_ThrowsSkipassStatusException()
    {
        //Arrange
        
        var sampleSkipassRecord = new SkipassRecord(1000, Guid.NewGuid(), Guid.NewGuid(), false);
        sampleSkipassRecord.Id = Guid.NewGuid();
        
        var visitorActionsRecord = new AddVisitorActionsModel(sampleSkipassRecord.Id, Guid.NewGuid());

        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new SkipassStatusException("Your skipass is inactive. Please, contact administrators"));

        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);
        
        //Assert
        SkipassRepositoryMock.Verify(m => 
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.SkipassId))));
        
        await action.Should().ThrowAsync<SkipassStatusException>();
    }

    [Fact]
    public async Task AddAsync_RelatedSkipassHasInvalidTariffId_ThrowsNotFoundException()
    {
        //Arrange
        var sampleSkipassRecord = new SkipassRecord(1000, Guid.NewGuid(), Guid.NewGuid(), true);
        sampleSkipassRecord.Id = Guid.NewGuid();
        
        var visitorActionsRecord = new AddVisitorActionsModel(sampleSkipassRecord.Id, Guid.NewGuid());
        
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("couldn't find tariff related to the current skipass"));
        
        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);
        
        //Assert
        await action.Should().ThrowAsync<NotFoundException>();

        TariffsRepositoryMock.Verify(m => 
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(sampleSkipassRecord.TariffId))));
    }

    [Fact]
    public async Task AddAsync_RelatedToVisitorActionSkipassHasInvalidLocationId_ThrowsNotFoundException()
    {
        //Arrange
        var sampleSkipassRecord = new SkipassRecord(1000, Guid.NewGuid(), Guid.NewGuid(), true);
        sampleSkipassRecord.Id = Guid.NewGuid();

        var sampleLocationRecord = new LocationRecord("name");
        sampleLocationRecord.Id = new Guid();
        
        var sampleTariffRecord = new TariffRecord("name", 1, false);
        sampleTariffRecord.Tariffications.Append(new TarifficationRecord(100, sampleTariffRecord.Id, sampleLocationRecord.Id));
        
        
        var visitorActionsRecord = new AddVisitorActionsModel(sampleSkipassRecord.Id, sampleLocationRecord.Id);
        SkipassRepositoryMock.Setup(m => m.GetByIdAsync(sampleSkipassRecord.Id))
            .ReturnsAsync(sampleSkipassRecord);

        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(sampleTariffRecord);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(visitorActionsRecord.LocationId))
            .ThrowsAsync(new NotFoundException("current tariff doesn't set the price for that location"));
        
        //Act
        var action = () => VisitorActionsService.AddAsync(visitorActionsRecord);
        
        //Assert
        await action.Should().ThrowAsync<NotFoundException>("current tariff doesn't set the price for that location");
        
        LocationRepositoryMock.Verify(m => 
            m.GetByIdAsync(It.Is<Guid>(v => v.Equals(visitorActionsRecord.LocationId))));
    }

    [Fact]
    public async Task AddAsync_ModelWithRelatedTariffAndSkipassThatDontShareATarrification_ThrowsNotFoundException()
    {
        //Arrange
        var skipassEntity = new SkipassRecord(1000, Guid.NewGuid(), Guid.NewGuid(), true);
        var tariffEntity = new TariffRecord(FixtureGenerator.Create<string>(), 1, false);
        var locationEntity = FixtureGenerator.Create<LocationRecord>();
        tariffEntity.Tariffications.Append(new TarifficationRecord(100, tariffEntity.Id, locationEntity.Id));

        var model = new AddVisitorActionsModel(skipassEntity.Id, locationEntity.Id);
        VisitorActionsRepositoryMock.Setup(m =>
            m.AddAsync(It.IsAny<VisitorActionsRecord>())).ReturnsAsync(Guid.NewGuid());
        
        VisitorActionsRepositoryMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1);

        SkipassRepositoryMock.Setup(m =>
            m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(skipassEntity);
        
        TariffsRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(tariffEntity);

        LocationRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new NotFoundException("couldn't find tariffication related to such tariff and location"));

        //Act
        var action = () => VisitorActionsService.AddAsync(model);
        
        //Assert
        await action.Should()
            .ThrowAsync<NotFoundException>("couldn't find tariffication related to such tariff and location");
    }
}