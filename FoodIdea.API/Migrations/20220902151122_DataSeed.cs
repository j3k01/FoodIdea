using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodIdea.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientsClass");

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Włoskie danie z makaronem i mięsem mielonym", "Spaghetti" });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Meksykańskie danie z fasoli czerwonej, białej, mięsa mielonego oraz kukurydzy", "Chilli con-carne" });

            migrationBuilder.InsertData(
                table: "Foods",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Danie składające się z losowych warzyw, ryżu, opcjonalnie jakieś mięso", "Mix warzywny" });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "IsHardToDo", "PreparationTime", "TasteRate" },
                values: new object[] { 1, 1, false, 30, 8.0 });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "IsHardToDo", "PreparationTime", "TasteRate" },
                values: new object[] { 2, 2, false, 20, 8.5 });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "FoodId", "IsHardToDo", "PreparationTime", "TasteRate" },
                values: new object[] { 3, 3, false, 20, 7.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Recipes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Foods",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.CreateTable(
                name: "IngredientsClass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Ingredient = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    RecipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientsClass_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientsClass_RecipeId",
                table: "IngredientsClass",
                column: "RecipeId");
        }
    }
}
