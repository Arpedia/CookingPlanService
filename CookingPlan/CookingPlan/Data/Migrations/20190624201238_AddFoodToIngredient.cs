using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CookingPlan.Data.Migrations
{
    public partial class AddFoodToIngredient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_Food_FoodId",
                table: "Ingredient");

            migrationBuilder.DropTable(
                name: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Ingredient_FoodId",
                table: "Ingredient");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Ingredient");

            migrationBuilder.AddColumn<string>(
                name: "Food",
                table: "Ingredient",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Food",
                table: "Ingredient");

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Ingredient",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredient_FoodId",
                table: "Ingredient",
                column: "FoodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_Food_FoodId",
                table: "Ingredient",
                column: "FoodId",
                principalTable: "Food",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
