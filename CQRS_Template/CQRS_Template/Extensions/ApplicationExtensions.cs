namespace CQRS_Template.Extensions;

public static class ApplicationExtensions
{
    public static async Task UseDbInitializerAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();

        await context.Database.MigrateAsync();

        if (!await context.Employees.AnyAsync())
        {
            await context.Employees.AddRangeAsync(
                new EmployeeModel { Name = "Alexander", Department = "webdev" },
                new EmployeeModel { Name = "Tatiana",   Department = "devops" },
                new EmployeeModel { Name = "Olga",      Department = "tester" });

            await context.SaveChangesAsync();
        }
    }
}
