using Application.Infrastructure.Automapper;

namespace Web.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(WebProfile));
        services.AddAutoMapper(typeof(ApplicationProfile));
        return services;
    }
}