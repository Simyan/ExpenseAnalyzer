using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    public partial class addBankAndReportMetadata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankMasterUid",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BankMaster",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankMaster", x => x.UId);
                });

            migrationBuilder.CreateTable(
                name: "ReportMetadataMaster",
                columns: table => new
                {
                    UId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TableArea = table.Column<double>(type: "float", nullable: false),
                    TableIndex = table.Column<int>(type: "int", nullable: false),
                    RowIndex = table.Column<int>(type: "int", nullable: false),
                    BankMasterUId = table.Column<int>(type: "int", nullable: false),
                    TableHeaders = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportMetadataMaster", x => x.UId);
                    table.ForeignKey(
                        name: "FK_ReportMetadataMaster_BankMaster_BankMasterUId",
                        column: x => x.BankMasterUId,
                        principalTable: "BankMaster",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_BankMasterUid",
                table: "User",
                column: "BankMasterUid");

            migrationBuilder.CreateIndex(
                name: "IX_ReportMetadataMaster_BankMasterUId",
                table: "ReportMetadataMaster",
                column: "BankMasterUId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_BankMaster_BankMasterUid",
                table: "User",
                column: "BankMasterUid",
                principalTable: "BankMaster",
                principalColumn: "UId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_BankMaster_BankMasterUid",
                table: "User");

            migrationBuilder.DropTable(
                name: "ReportMetadataMaster");

            migrationBuilder.DropTable(
                name: "BankMaster");

            migrationBuilder.DropIndex(
                name: "IX_User_BankMasterUid",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BankMasterUid",
                table: "User");
        }
    }
}
