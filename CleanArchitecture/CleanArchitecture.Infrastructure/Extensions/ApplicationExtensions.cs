namespace CleanArchitecture.Infrastructure.Extensions;

public static class ApplicationExtensions
{
    public static async Task<IApplicationBuilder> UseDbInitializerAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<DatabaseContext<Employee>>();

        await context.Database.MigrateAsync();

        if (!await context.Entities.AnyAsync())
        {
            await context.Entities.AddRangeAsync(
                new Employee { Name = "Alexander", Department = "webdev" },
                new Employee { Name = "Tatiana",   Department = "devops" },
                new Employee { Name = "Olga",      Department = "tester" });

            await context.SaveChangesAsync();
        }

        return app;
    }
}
