using Application.Skipass;
using Application.Tariff;
using Application.Visitor;
using Application.VisitorAction;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<ISkipassService, SkipassService>();
        services.AddTransient<ITariffService, TariffService>();
        services.AddTransient<IVisitorService, VisitorService>();
        services.AddTransient<IVisitorActions, VisitorActionsService>();

        return services;
    }
    
}