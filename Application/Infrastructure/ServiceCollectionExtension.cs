using Application.Infrastructure.Automapper;
using Application.Skipass;
using Application.Tariff;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureSkipassServices(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>((provider, builder) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Psql");
            builder.UseNpgsql(connectionString);
        });

        services.AddTransient<ISkipassService, SkipassService>();

        return services;
    }
    public static IServiceCollection ConfigureTariffServices(this IServiceCollection services)
    {
        services.AddTransient<ITariffService, TariffService>();
        return services;
    }
}