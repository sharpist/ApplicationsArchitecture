#nullable disable

namespace CleanArchitecture.Migrations.Migrations;

[DbContext(typeof(DatabaseContext<Employee>))]
partial class DatabaseContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.1")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("CleanArchitecture.Domain.Entities.Employee", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

            b.Property<DateTime?>("Created")
                .ValueGeneratedOnAdd()
                .HasColumnType("datetime2")
                .HasColumnName("Created");

            b.Property<string>("Department")
                .HasColumnType("nvarchar(100)")
                .HasColumnName("Department");

            b.Property<DateTime?>("Modified")
                .ValueGeneratedOnUpdate()
                .HasColumnType("datetime2")
                .HasColumnName("Modified");

            b.Property<string>("Name")
                .HasColumnType("nvarchar(100)")
                .HasColumnName("Name");

            b.HasKey("Id");

            b.ToTable("Employees", (string)null);

            b.HasData(
                new
                {
                    Id = 1,
                    Created = new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3342),
                    Department = "webdev",
                    Name = "Alexander"
                },
                new
                {
                    Id = 2,
                    Created = new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3351),
                    Department = "devops",
                    Name = "Tatiana"
                },
                new
                {
                    Id = 3,
                    Created = new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3353),
                    Department = "tester",
                    Name = "Olga"
                });
        });
#pragma warning restore 612, 618
    }
}
