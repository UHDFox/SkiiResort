using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using SkiiResort.Application.Infrastructure.Automapper;
using SkiiResort.Repository.Location;
using SkiiResort.Repository.Skipass;
using SkiiResort.Repository.Tariff;
using SkiiResort.Repository.Tariffication;
using SkiiResort.Repository.Visitor;
using SkiiResort.Repository.VisitorActions;

namespace SkiiResort.Tests;

public abstract class TestBase
{
    protected Mock<IVisitorRepository> VisitorsRepositoryMock;

    protected Mock<ITariffRepository> TariffRepositoryMock;

    protected Mock<ILocationRepository> LocationRepositoryMock;

    protected Mock<ISkipassRepository> SkipassRepositoryMock;

    protected Mock<ITarifficationRepository> TarifficationRepositoryMock;

    protected Mock<IVisitorActionsRepository> VisitorActionsRepositoryMock;

    protected IMapper Mapper;

    protected readonly IFixture FixtureGenerator;

    protected TestBase()
    {
        FixtureGenerator = new Fixture().Customize(new AutoMoqCustomization());
        FixtureGenerator.Behaviors.Remove(new ThrowingRecursionBehavior());
        FixtureGenerator.Behaviors.Add(new OmitOnRecursionBehavior());

        LocationRepositoryMock = FixtureGenerator.Freeze<Mock<ILocationRepository>>();
        VisitorsRepositoryMock = FixtureGenerator.Freeze<Mock<IVisitorRepository>>();
        TariffRepositoryMock = FixtureGenerator.Freeze<Mock<ITariffRepository>>();
        SkipassRepositoryMock = FixtureGenerator.Freeze<Mock<ISkipassRepository>>();
        TarifficationRepositoryMock = FixtureGenerator.Freeze<Mock<ITarifficationRepository>>();
        VisitorActionsRepositoryMock = FixtureGenerator.Freeze<Mock<IVisitorActionsRepository>>();

        Mapper = new MapperConfiguration(c =>
        {
            c.AddProfile(new ApplicationProfile());
        }).CreateMapper();

        FixtureGenerator.Register(() => Mapper);
        FixtureGenerator.Register(() => VisitorsRepositoryMock);
        FixtureGenerator.Register(() => LocationRepositoryMock);
        FixtureGenerator.Register(() => TariffRepositoryMock);
        FixtureGenerator.Register(() => SkipassRepositoryMock);
        FixtureGenerator.Register(() => TarifficationRepositoryMock);
        FixtureGenerator.Register(() => VisitorActionsRepositoryMock);
    }
}
