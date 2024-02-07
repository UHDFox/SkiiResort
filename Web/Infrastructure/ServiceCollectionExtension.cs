using Application.Infrastructure.Automapper;
using Newtonsoft.Json;
using Web.Infrastructure.Automapper;

namespace Web.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddAutomapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(WebProfile));
        services.AddAutoMapper(typeof(ApplicationProfile));
        return services;
    }
}