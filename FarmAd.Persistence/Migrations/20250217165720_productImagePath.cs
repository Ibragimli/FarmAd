using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAd.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class productImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "ProductImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "ProductImages");
        }
    }
}
