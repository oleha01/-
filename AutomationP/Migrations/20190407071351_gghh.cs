using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class gghh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProductId",
                table: "Sales");

            migrationBuilder.AddColumn<string>(
                name: "Coment",
                table: "Sales",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalesId",
                table: "Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SalesId",
                table: "Products",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Sales_SalesId",
                table: "Products",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Sales_SalesId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SalesId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Coment",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "Products");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Products_ProductId",
                table: "Sales",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
