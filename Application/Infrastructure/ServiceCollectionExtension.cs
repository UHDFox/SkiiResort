using Application.Skipass;
using Application.Tariff;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureSkipassServices(this IServiceCollection services)
    {
        services.AddTransient<ISkipassService, SkipassService>();

        return services;
    }
    public static IServiceCollection ConfigureTariffServices(this IServiceCollection services)
    {
        services.AddTransient<ITariffService, TariffService>();
        return services;
    }
}