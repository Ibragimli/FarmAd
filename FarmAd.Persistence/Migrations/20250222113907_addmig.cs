using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarmAd.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class addmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_Cities_CityId1",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductFeatures_SubCategories_SubCategoryId1",
                table: "ProductFeatures");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId1",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductImages_ProductId1",
                table: "ProductImages");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_CityId1",
                table: "ProductFeatures");

            migrationBuilder.DropIndex(
                name: "IX_ProductFeatures_SubCategoryId1",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "SubCategoryId1",
                table: "ProductFeatures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId1",
                table: "ProductImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "ProductFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SubCategoryId1",
                table: "ProductFeatures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductImages_ProductId1",
                table: "ProductImages",
                column: "ProductId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_CityId1",
                table: "ProductFeatures",
                column: "CityId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProductFeatures_SubCategoryId1",
                table: "ProductFeatures",
                column: "SubCategoryId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_Cities_CityId1",
                table: "ProductFeatures",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductFeatures_SubCategories_SubCategoryId1",
                table: "ProductFeatures",
                column: "SubCategoryId1",
                principalTable: "SubCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId1",
                table: "ProductImages",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
