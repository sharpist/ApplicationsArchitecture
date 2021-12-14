﻿#nullable disable

namespace CleanArchitecture.Migrations.Migrations;

[DbContext(typeof(DatabaseContext<Employee>))]
[Migration("20211212114644_Initial")]
partial class Initial
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder)
    {
#pragma warning disable 612, 618
        modelBuilder
            .HasAnnotation("ProductVersion", "6.0.0")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

        modelBuilder.Entity("CleanArchitecture.Domain.Entities.Employee", b =>
        {
            b.Property<int>("Id")
                .ValueGeneratedOnAdd()
                .HasColumnType("int");

            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

            b.Property<DateTime>("AddedOn")
                .HasColumnType("datetime2");

            b.Property<string>("Department")
                .HasColumnType("nvarchar(max)");

            b.Property<string>("Name")
                .HasColumnType("nvarchar(max)");

            b.HasKey("Id");

            b.ToTable("Entities");
        });
#pragma warning restore 612, 618
    }
}