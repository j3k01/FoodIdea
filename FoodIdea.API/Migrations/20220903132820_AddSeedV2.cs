using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodIdea.API.Migrations
{
    public partial class AddSeedV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HowToMake",
                table: "Recipes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1,
                column: "HowToMake",
                value: "Ugotuj makaron, usmaz mieso, dodaj sos");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2,
                column: "HowToMake",
                value: "Usmaz mieso, dodaj fasole, zrob sos");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "HowToMake", "TasteRate" },
                values: new object[] { "Usmaz mieso, dodaj warzywa, zrob sos", 7.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HowToMake",
                table: "Recipes");

            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TasteRate",
                value: 1.0);
        }
    }
}
