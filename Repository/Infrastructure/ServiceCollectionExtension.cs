using Microsoft.Extensions.DependencyInjection;
using SkiiResort.Repository.Location;
using SkiiResort.Repository.Skipass;
using SkiiResort.Repository.Tariff;
using SkiiResort.Repository.Tariffication;
using SkiiResort.Repository.Visitor;
using SkiiResort.Repository.VisitorActions;

namespace SkiiResort.Repository.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ISkipassRepository, SkipassRepository>();

        services.AddTransient<ITariffRepository, TariffRepository>();

        services.AddTransient<IVisitorRepository, VisitorRepository>();

        services.AddTransient<IVisitorActionsRepository, VisitorActionsRepository>();

        services.AddTransient<ILocationRepository, LocationRepository>();

        services.AddTransient<ITarifficationRepository, TarifficationRepository>();


        return services;
    }
}
