using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAd.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addsettingimagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Settings");
        }
    }
}
