using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class Some : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Invoice_Products",
                newName: "Quantity");

            migrationBuilder.AddColumn<int>(
                name: "Prise",
                table: "Invoice_Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "IncomingInvoices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IncomingInvoices_UserId",
                table: "IncomingInvoices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomingInvoices_Users_UserId",
                table: "IncomingInvoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomingInvoices_Users_UserId",
                table: "IncomingInvoices");

            migrationBuilder.DropIndex(
                name: "IX_IncomingInvoices_UserId",
                table: "IncomingInvoices");

            migrationBuilder.DropColumn(
                name: "Prise",
                table: "Invoice_Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "IncomingInvoices");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Invoice_Products",
                newName: "Capacity");
        }
    }
}
