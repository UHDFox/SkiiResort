using Microsoft.Extensions.DependencyInjection;
using Repository.Skipass;

namespace Repository.Infrastructure;

public static class ServiceCollectionExtension 
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient<ISkipassRepository, SkipassRepository>();
        
        return services;
    }
}