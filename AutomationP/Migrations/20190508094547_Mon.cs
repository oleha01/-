using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class Mon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Money_Sales_SalesId",
                table: "Money");

            migrationBuilder.DropIndex(
                name: "IX_Money_SalesId",
                table: "Money");

            migrationBuilder.DropColumn(
                name: "SalesId",
                table: "Money");

            migrationBuilder.RenameColumn(
                name: "Data",
                table: "Money",
                newName: "Date");

            migrationBuilder.AddColumn<int>(
                name: "PointId",
                table: "Money",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Money_PointId",
                table: "Money",
                column: "PointId");

            migrationBuilder.AddForeignKey(
                name: "FK_Money_PointOfSales_PointId",
                table: "Money",
                column: "PointId",
                principalTable: "PointOfSales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Money_PointOfSales_PointId",
                table: "Money");

            migrationBuilder.DropIndex(
                name: "IX_Money_PointId",
                table: "Money");

            migrationBuilder.DropColumn(
                name: "PointId",
                table: "Money");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Money",
                newName: "Data");

            migrationBuilder.AddColumn<int>(
                name: "SalesId",
                table: "Money",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Money_SalesId",
                table: "Money",
                column: "SalesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Money_Sales_SalesId",
                table: "Money",
                column: "SalesId",
                principalTable: "Sales",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
