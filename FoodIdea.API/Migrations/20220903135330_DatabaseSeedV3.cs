using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodIdea.API.Migrations
{
    public partial class DatabaseSeedV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "HowToMake",
                value: "Usmaz mieso, dodaj warzywa, dodaj odpowiednie przyprawy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "HowToMake",
                value: "Usmaz mieso, dodaj warzywa, zrob sos");
        }
    }
}
