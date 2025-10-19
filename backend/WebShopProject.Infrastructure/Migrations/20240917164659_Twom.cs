using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShopProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Twom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_StreetAndNumber",
                table: "Users",
                newName: "Address_Street");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "Users",
                newName: "Address_StreetAndNumber");
        }
    }
}
