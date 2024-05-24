using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Migrations
{
    /// <inheritdoc />
    public partial class DishAllergenCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergen",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllergenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergenImageURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergen", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DishAllergen",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishID = table.Column<int>(type: "int", nullable: false),
                    AllergenID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishAllergen", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DishAllergen_Allergen_AllergenID",
                        column: x => x.AllergenID,
                        principalTable: "Allergen",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DishAllergen_Dish_DishID",
                        column: x => x.DishID,
                        principalTable: "Dish",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DishAllergen_AllergenID",
                table: "DishAllergen",
                column: "AllergenID");

            migrationBuilder.CreateIndex(
                name: "IX_DishAllergen_DishID",
                table: "DishAllergen",
                column: "DishID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishAllergen");

            migrationBuilder.DropTable(
                name: "Allergen");
        }
    }
}
