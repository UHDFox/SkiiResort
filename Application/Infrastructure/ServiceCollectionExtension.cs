using Application.Skipass;
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

        services.AddTransient<ISkipassService>(provider => provider.GetRequiredService<SkipassService>());

        return services;
    }
}