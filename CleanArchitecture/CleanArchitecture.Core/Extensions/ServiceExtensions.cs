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
        services.AddScoped<ICommandHandler<PostEmployeeCommand>, EmployeeCommandHandler>();

        services.AddScoped<IQueryHandler<GetEmployeesQuery, IEnumerable<ReadEmployeeDTO>>, EmployeeQueryHandler>();

        services.AddScoped<IQueryHandler<GetEmployeeByIdQuery, ReadEmployeeDTO>, EmployeeQueryHandler>();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        return services;
    }
}
