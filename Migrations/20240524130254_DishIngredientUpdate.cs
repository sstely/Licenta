using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Licenta.Migrations
{
    /// <inheritdoc />
    public partial class DishIngredientUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "DishIngredient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "DishIngredient",
                type: "decimal(6,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
