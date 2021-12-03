﻿#nullable disable

namespace CQRS_Template.Migrations;

[DbContext(typeof(EmployeeDbContext))]
partial class EmployeeDbContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("EmployeeModel", b =>
        {
            b.Property<int>("EmployeeId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmployeeId"), 1L, 1);

            b.Property<string>("Department")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.Property<string>("Name")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            b.HasKey("EmployeeId");

            b.ToTable("Employee");
        });
#pragma warning restore 612, 618
    }
}
