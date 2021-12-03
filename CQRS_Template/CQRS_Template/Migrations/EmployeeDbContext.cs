#nullable disable

namespace CQRS_Template.Migrations;

public class EmployeeDbContext : DbContext
{
    public DbSet<EmployeeModel> Employees { get; set; }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
    { }
}
