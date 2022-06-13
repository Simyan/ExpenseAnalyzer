using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserUid",
                table: "Vendor",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserUid",
                table: "Transaction",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_UserUid",
                table: "Vendor",
                column: "UserUid");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserUid",
                table: "Transaction",
                column: "UserUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_UserUid",
                table: "Transaction",
                column: "UserUid",
                principalTable: "User",
                principalColumn: "UId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_User_UserUid",
                table: "Vendor",
                column: "UserUid",
                principalTable: "User",
                principalColumn: "UId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_UserUid",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_User_UserUid",
                table: "Vendor");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_UserUid",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserUid",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserUid",
                table: "Vendor");

            migrationBuilder.DropColumn(
                name: "UserUid",
                table: "Transaction");
        }
    }
}
