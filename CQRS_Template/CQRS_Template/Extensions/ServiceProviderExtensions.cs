namespace CQRS_Template.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddDbContext<AppDbContext<EmployeeModel>>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection"));
        });

        services.AddTransient<IRepository<EmployeeModel>, Repository<EmployeeModel>>();

        return services;
    }

    public static IServiceCollection AddCQRS(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<PostEmployeeCommand>, EmployeeCommandHandler>();

        services.AddScoped<IQueryHandler<GetEmployeesQuery, EmployeeModel[]>, EmployeeQueryHandler>();

        services.AddScoped<IQueryHandler<GetEmployeeByIdQuery, EmployeeModel>, EmployeeQueryHandler>();

        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();

        services.AddSingleton<IQueryDispatcher, QueryDispatcher>();

        return services;
    }
}
