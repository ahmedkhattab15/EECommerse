using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Test_System.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_productColors_Products_ProductID",
                table: "productColors",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubImages_Products_ProductID",
                table: "ProductSubImages",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productColors_Products_ProductID",
                table: "productColors");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubImages_Products_ProductID",
                table: "ProductSubImages");
        }
    }
}
