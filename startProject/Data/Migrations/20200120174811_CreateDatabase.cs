using Microsoft.EntityFrameworkCore.Migrations;

namespace startProject.Data.Migrations
{
    public partial class CreateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    WeekNrFlowerStart = table.Column<int>(nullable: false),
                    WeekNrFlowerEnd = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 1, "krokus", 15, 8 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 2, "narcis", 20, 10 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 3, "hyacint", 30, 15 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 4, "tulp", 40, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 5, "violet", 45, 25 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 6, "roos", 42, 30 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 7, "zonnebloem", 32, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 8, "margriet", 28, 18 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 9, "goudsbloem", 30, 20 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name", "WeekNrFlowerEnd", "WeekNrFlowerStart" },
                values: new object[] { 10, "Clematis ", 35, 30 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
