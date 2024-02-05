using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddHotelContext(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>((provider, builder) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Psql");
            builder.UseNpgsql(connectionString);
        });

        return services;
    }
}