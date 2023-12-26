using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Domain.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureDataBase(this IServiceCollection services)
    {
        services.AddDbContext<HotelContext>((provider, builder) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Psql");
            builder.UseNpgsql(connectionString);
        });

        return services;
    }
}