using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Migrations
{
    /// <inheritdoc />
    public partial class DishIngredientCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IngredientPrice = table.Column<decimal>(type: "decimal(6,2)", nullable: false),
                    IngredientPresentation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    SaturatedFats = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    UnsaturatedFats = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Sugars = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Proteins = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Salt = table.Column<decimal>(type: "decimal(6,2)", nullable: true),
                    Fibers = table.Column<decimal>(type: "decimal(6,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DishIngredient",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishID = table.Column<int>(type: "int", nullable: false),
                    IngredientID = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(6,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishIngredient", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DishIngredient_Dish_DishID",
                        column: x => x.DishID,
                        principalTable: "Dish",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishIngredient_Ingredient_IngredientID",
                        column: x => x.IngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredient_DishID",
                table: "DishIngredient",
                column: "DishID");

            migrationBuilder.CreateIndex(
                name: "IX_DishIngredient_IngredientID",
                table: "DishIngredient",
                column: "IngredientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishIngredient");

            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
