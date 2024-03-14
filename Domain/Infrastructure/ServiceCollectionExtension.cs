using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddSkiiResortContext(this IServiceCollection services)
    {
        services.AddDbContext<SkiiResortContext>((provider, builder) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Psql");
            builder.UseNpgsql(connectionString);
        });

        return services;
    }
}