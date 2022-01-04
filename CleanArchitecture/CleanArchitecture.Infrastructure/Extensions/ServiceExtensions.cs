﻿namespace CleanArchitecture.Infrastructure.Extensions;

public static class ServiceExtensions
{
    private static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDbContext<DatabaseContext<Employee>>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EmployeeDbConnection"), options =>
            {
                options.EnableRetryOnFailure(3, TimeSpan.FromSeconds(3), null);
                options.MigrationsAssembly(typeof(DatabaseContext<>).Assembly.FullName);
            });
        });
    }

    private static IServiceCollection AddRepository(this IServiceCollection services)
    {
        return services.AddScoped<IRepository<Employee>, Repository<Employee>>();
    }

    public static IServiceCollection AddStorage(this IServiceCollection services, IConfiguration configuration)
    {
        return services.AddDatabaseContext(configuration).AddRepository();
    }
}
