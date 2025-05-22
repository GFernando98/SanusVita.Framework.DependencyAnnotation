using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SanusVita.Framework.DependencyAnnotation.DependencyAnnotation;

public static class DependecyAnnotationService
{
    public static IServiceCollection AddDependencyAnnotation(this IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        foreach (var assembly in assemblies)
        {
            Type[] types;
            try
            {
                types = assembly.GetTypes();
            }
            catch (ReflectionTypeLoadException ex)
            {
                types = ex.Types.Where(t => t != null).ToArray()!;
            }
            catch
            {
                continue;
            }

            var typesWithScopeService = types
                .Where(t => t.GetCustomAttributes(typeof(ScopeService), true).Any());
            foreach (var type in typesWithScopeService)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                    foreach (var iface in interfaces)
                        services.AddScoped(iface, type);
                else services.AddScoped(type);
            }

            var typesWithSingletonService = types
                .Where(t => t.GetCustomAttributes(typeof(SingletonService), true).Any());
            foreach (var type in typesWithSingletonService)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                    foreach (var iface in interfaces)
                        services.AddSingleton(iface, type);
                else services.AddSingleton(type);
            }

            var typesWithTransientService = types
                .Where(t => t.GetCustomAttributes(typeof(TransientService), true).Any());
            foreach (var type in typesWithTransientService)
            {
                var interfaces = type.GetInterfaces();
                if (interfaces.Any())
                    foreach (var iface in interfaces)
                        services.AddTransient(iface, type);
                else services.AddTransient(type);
            }
        }

        return services;
    }
}
