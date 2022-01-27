#nullable disable

namespace CleanArchitecture.Migrations.Migrations;

public partial class Initial : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Employees",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                Department = table.Column<string>(type: "nvarchar(100)", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: true),
                Modified = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Employees", x => x.Id);
            });

        migrationBuilder.InsertData(
            table: "Employees",
            columns: new[] { "Id", "Created", "Department", "Modified", "Name" },
            values: new object[] { 1, new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3342), "webdev", null, "Alexander" });

        migrationBuilder.InsertData(
            table: "Employees",
            columns: new[] { "Id", "Created", "Department", "Modified", "Name" },
            values: new object[] { 2, new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3351), "devops", null, "Tatiana" });

        migrationBuilder.InsertData(
            table: "Employees",
            columns: new[] { "Id", "Created", "Department", "Modified", "Name" },
            values: new object[] { 3, new DateTime(2022, 1, 27, 17, 5, 48, 969, DateTimeKind.Utc).AddTicks(3353), "tester", null, "Olga" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Employees");
    }
}
