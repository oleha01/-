using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class SalesR1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Sales_Products",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_Products_UserId",
                table: "Sales_Products",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Products_Users_UserId",
                table: "Sales_Products",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Products_Users_UserId",
                table: "Sales_Products");

            migrationBuilder.DropIndex(
                name: "IX_Sales_Products_UserId",
                table: "Sales_Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Sales_Products");
        }
    }
}
