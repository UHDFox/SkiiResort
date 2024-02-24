using System.Reflection;
using Application.Location;
using Application.Skipass;
using Application.Tariff;
using Application.Tariffication;
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

        services.AddTransient<ILocationService, LocationService>();

        services.AddTransient<ITarifficationService, TarifficationService>();
        
        services.AddAutoMapper(typeof(ApplicationAssemblyReference).Assembly);

        
        return services;
    }

    private static class ApplicationAssemblyReference
    {
        public static Assembly Assembly => typeof(ApplicationAssemblyReference).Assembly;
    }
}