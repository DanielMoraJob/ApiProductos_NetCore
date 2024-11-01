using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiProductos.Migrations
{
    /// <inheritdoc />
    public partial class modifymodelproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Products",
                newName: "Stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "Number");
        }
    }
}
