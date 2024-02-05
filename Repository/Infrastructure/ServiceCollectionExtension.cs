using Microsoft.Extensions.DependencyInjection;
using Repository.Skipass;
using Repository.Tariff;
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
        return services;
    }
}