namespace CQRS_Template.Extensions;

public static class ServiceProviderExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services, WebApplicationBuilder builder)
    {
        return services.AddDbContext<EmployeeDbContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbConnection"));
        });
    }
}
