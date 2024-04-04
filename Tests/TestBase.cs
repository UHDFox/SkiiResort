using Application.Infrastructure.Automapper;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using Repository.Location;
using Repository.Skipass;
using Repository.Tariff;
using Repository.Tariffication;
using Repository.Visitor;

namespace Tests;

public abstract class TestBase
{
    protected internal Mock<IVisitorRepository> VisitorsRepositoryMock;
    
    protected internal Mock<ITariffRepository> TariffsRepositoryMock;

    protected internal Mock<ILocationRepository> LocationRepositoryMock;

    protected internal Mock<ISkipassRepository> SkipassRepositoryMock;

    protected internal Mock<ITarifficationRepository> TarifficationRepositoryMock;
    
    protected internal  IMapper Mapper;

    protected internal readonly IFixture FixtureGenerator;

    protected TestBase()
    {
        FixtureGenerator = new Fixture().Customize(new AutoMoqCustomization());
        FixtureGenerator.Behaviors.Remove(new ThrowingRecursionBehavior());
        FixtureGenerator.Behaviors.Add(new OmitOnRecursionBehavior());
        
        LocationRepositoryMock = FixtureGenerator.Freeze<Mock<ILocationRepository>>();
        VisitorsRepositoryMock = FixtureGenerator.Freeze<Mock<IVisitorRepository>>();
        TariffsRepositoryMock = FixtureGenerator.Freeze<Mock<ITariffRepository>>();
        SkipassRepositoryMock = FixtureGenerator.Freeze<Mock<ISkipassRepository>>();
        TarifficationRepositoryMock = FixtureGenerator.Freeze<Mock<ITarifficationRepository>>();
        
        
        Mapper = new MapperConfiguration(c =>
        {
            c.AddProfile(new ApplicationProfile());
        }).CreateMapper();
        
        FixtureGenerator.Register(() => Mapper);
        FixtureGenerator.Register(() => VisitorsRepositoryMock);
        FixtureGenerator.Register(() => LocationRepositoryMock);
        FixtureGenerator.Register(() => TariffsRepositoryMock);
        FixtureGenerator.Register(() => SkipassRepositoryMock);
        FixtureGenerator.Register(() => TarifficationRepositoryMock);
    }
}