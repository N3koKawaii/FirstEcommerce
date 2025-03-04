using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "ProductTypes",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "ProductBrands",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductTypes",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductBrands",
                newName: "name");
        }
    }
}
