using Microsoft.Extensions.DependencyInjection;
using Repository.Location;
using Repository.Skipass;
using Repository.Tariff;
using Repository.Tariffication;
using Repository.Visitor;
using Repository.VisitorActions;

namespace Repository.Infrastructure;

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