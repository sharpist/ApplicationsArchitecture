namespace CleanArchitecture.Migrations.Configuration;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("Employees");

        builder.Property(e => e.Id)
            .IsRequired(true)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Name)
            .HasColumnName("Name")
            .HasColumnType("nvarchar(100)")
            .IsRequired(false);

        builder.Property(e => e.Department)
            .HasColumnName("Department")
            .HasColumnType("nvarchar(100)")
            .IsRequired(false);

        builder.Property(e => e.Created)
            .HasColumnName("Created")
            .IsRequired(false)
            .ValueGeneratedOnAdd();

        builder.Property(e => e.Modified)
            .HasColumnName("Modified")
            .IsRequired(false)
            .ValueGeneratedOnUpdate();

        builder.HasData
        (
            new Employee
            {
                Id = 1,
                Name = "Alexander",
                Department = "webdev",
                Created = DateTime.UtcNow
            },
            new Employee
            {
                Id = 2,
                Name = "Tatiana",
                Department = "devops",
                Created = DateTime.UtcNow
            },
            new Employee
            {
                Id = 3,
                Name = "Olga",
                Department = "tester",
                Created = DateTime.UtcNow
            }
        );
    }
}
