namespace CleanArchitecture.Core.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        return services
            .AddSingleton<ICommandDispatcher, CommandDispatcher>()
            .AddSingleton<IQueryDispatcher, QueryDispatcher>()
            .RegisterGenericTypes(ServiceLifetime.Scoped, typeof(ICommandHandler<>), typeof(IQueryHandler<,>));
    }

    private static IServiceCollection RegisterGenericTypes(this IServiceCollection services, ServiceLifetime lifetime, params Type[] types)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        if (assemblies is not null && assemblies.Length is not 0)
        {
            var allTypes = assemblies
                .Where(a => !a.IsDynamic && a.GetName().Name == typeof(ServiceExtensions).Assembly.GetName().Name)
                .Distinct()
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            foreach (var implementationType in types
                .SelectMany(openType => allTypes
                .Where(t => t.IsClass && !t.IsAbstract && t.AsType().ImplementsGenericInterface(openType))))
            {
                Array.ForEach(implementationType.ImplementedInterfaces.ToArray(), @interface =>
                    services.TryAdd(new ServiceDescriptor(@interface, implementationType.AsType(), lifetime)));
            }
        }

        return services;
    }

    private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
    {
        if (!type.IsGenericType(interfaceType))
        {
            return type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));
        }

        return true;
    }

    private static bool IsGenericType(this Type type, Type genericType)
    {
        if (type.GetTypeInfo().IsGenericType)
        {
            return type.GetGenericTypeDefinition() == genericType;
        }

        return false;
    }
}
