namespace CleanArchitecture.Core.Extensions;

public static class ApplicationExtensions
{
    public static async Task UseDbInitializerAsync(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext<Employee>>();

        await context.Database.MigrateAsync();

        if (!await context.Entities.AnyAsync())
        {
            await context.Entities.AddRangeAsync(
                new Employee { Name = "Alexander", Department = "webdev" },
                new Employee { Name = "Tatiana",   Department = "devops" },
                new Employee { Name = "Olga",      Department = "tester" });

            await context.SaveChangesAsync();
        }
    }
}
