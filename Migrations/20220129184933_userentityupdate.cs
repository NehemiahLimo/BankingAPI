using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingAPI.Migrations
{
    public partial class userentityupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Customers");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LastUpdatedBy",
                table: "Customers",
                column: "LastUpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_LastUpdatedBy",
                table: "Customers",
                column: "LastUpdatedBy",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_LastUpdatedBy",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LastUpdatedBy",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
