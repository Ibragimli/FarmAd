using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAd.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addmig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "UserAuthentications");

            migrationBuilder.RenameColumn(
                name: "Token",
                table: "UserAuthentications",
                newName: "Username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "UserAuthentications",
                newName: "Token");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "UserAuthentications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
