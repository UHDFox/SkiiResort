using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SkiiResort.Application.Infrastructure.Authentication;
using SkiiResort.Application.Location;
using SkiiResort.Application.Skipass;
using SkiiResort.Application.Tariff;
using SkiiResort.Application.Tariffication;
using SkiiResort.Application.User;
using SkiiResort.Application.Visitor;
using SkiiResort.Application.VisitorAction;

namespace SkiiResort.Application.Infrastructure;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddTransient<ISkipassService, SkipassService>();

        services.AddTransient<ITariffService, TariffService>();

        services.AddTransient<IVisitorService, VisitorService>();

        services.AddTransient<IVisitorActionsService, VisitorActionsService>();

        services.AddTransient<ILocationService, LocationService>();

        services.AddTransient<ITarifficationService, TarifficationService>();

        services.AddTransient<IUserService, UserService>();

        services.AddTransient<IJwtProvider, JwtProvider>();

        services.AddTransient<IPasswordProvider, PasswordProvider>();

        services.AddAutoMapper(ApplicationAssemblyReference.Assembly);


        return services;
    }

    private static class ApplicationAssemblyReference
    {
        public static Assembly Assembly => typeof(ApplicationAssemblyReference).Assembly;
    }
}
