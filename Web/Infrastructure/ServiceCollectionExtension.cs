using System.Reflection;

namespace Web.Infrastructure;

public static class ServiceCollectionExtension
{
    private static class ApplicationAssemblyReference
    {
        public static Assembly Assembly => typeof(ApplicationAssemblyReference).Assembly;
    }
}