using Microsoft.EntityFrameworkCore.Migrations;

namespace AutomationP.Migrations
{
    public partial class Role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleItems_Roles_RoleId",
                table: "RoleItems");

            migrationBuilder.DropIndex(
                name: "IX_RoleItems_RoleId",
                table: "RoleItems");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "RoleItems");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "RoleItems",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleItems_RoleId",
                table: "RoleItems",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleItems_Roles_RoleId",
                table: "RoleItems",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
