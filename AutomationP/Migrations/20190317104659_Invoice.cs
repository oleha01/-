using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class Invoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomingInvoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    StorageId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomingInvoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IncomingInvoices_Storages_StorageId",
                        column: x => x.StorageId,
                        principalTable: "Storages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    IncomingInvoiceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoice_Products_IncomingInvoices_IncomingInvoiceId",
                        column: x => x.IncomingInvoiceId,
                        principalTable: "IncomingInvoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Products_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomingInvoices_StorageId",
                table: "IncomingInvoices",
                column: "StorageId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Products_IncomingInvoiceId",
                table: "Invoice_Products",
                column: "IncomingInvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Products_ProductId",
                table: "Invoice_Products",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice_Products");

            migrationBuilder.DropTable(
                name: "IncomingInvoices");
        }
    }
}
