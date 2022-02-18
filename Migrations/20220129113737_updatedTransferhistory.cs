using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingAPI.Migrations
{
    public partial class updatedTransferhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransferedFromAccountNo",
                table: "TransferHistory",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TransferedToAccountNo",
                table: "TransferHistory",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransferedFromAccountNo",
                table: "TransferHistory");

            migrationBuilder.DropColumn(
                name: "TransferedToAccountNo",
                table: "TransferHistory");
        }
    }
}
