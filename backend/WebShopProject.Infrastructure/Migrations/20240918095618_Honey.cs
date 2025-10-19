using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Honey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Gender",
                table: "Products",
                newName: "KindOfHoney");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KindOfHoney",
                table: "Products",
                newName: "Gender");
        }
    }
}
