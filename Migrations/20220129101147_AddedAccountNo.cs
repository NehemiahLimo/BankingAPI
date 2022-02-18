using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingAPI.Migrations
{
    public partial class AddedAccountNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountNo",
                table: "Accounts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "Accounts");
        }
    }
}
