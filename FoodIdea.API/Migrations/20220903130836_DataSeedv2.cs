using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodIdea.API.Migrations
{
    public partial class DataSeedv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TasteRate",
                value: 1.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3,
                column: "TasteRate",
                value: 7.0);
        }
    }
}
