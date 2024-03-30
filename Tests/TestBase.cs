using AutoFixture;
using AutoFixture.AutoMoq;
using AutoMapper;
using Moq;
using Repository.Tariff;
using Repository.Visitor;
using Tests.AutomapperProfiles;

namespace Tests;

public abstract class TestBase
{
    protected internal Mock<IVisitorRepository> VisitorsRepositoryMock;
    
    protected internal Mock<ITariffRepository> TariffsRepositoryMock;
    
    protected internal  IMapper Mapper;

    protected internal readonly IFixture FixtureGenerator;

    protected TestBase()
    {
        FixtureGenerator = new Fixture().Customize(new AutoMoqCustomization());
        FixtureGenerator.Behaviors.Remove(new ThrowingRecursionBehavior());
        FixtureGenerator.Behaviors.Add(new OmitOnRecursionBehavior());

        VisitorsRepositoryMock = FixtureGenerator.Freeze<Mock<IVisitorRepository>>();
        TariffsRepositoryMock = FixtureGenerator.Freeze<Mock<ITariffRepository>>();
        
        Mapper = new MapperConfiguration(c =>
        {
            c.AddProfile(new TestMapperProfile());
        }).CreateMapper();
        
        FixtureGenerator.Register(() => Mapper);
        FixtureGenerator.Register(() => VisitorsRepositoryMock);

    }
}