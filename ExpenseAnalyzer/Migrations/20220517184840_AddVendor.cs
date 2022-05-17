using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    public partial class AddVendor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_CategoryMaster_CategoryUId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CategoryUId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CategoryUId",
                table: "Transaction");

            migrationBuilder.AddColumn<long>(
                name: "TransactionCategoryUid",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "VendorUId",
                table: "Transaction",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    UId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    DisplayName = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    CategoryMasterUid = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.UId);
                    table.ForeignKey(
                        name: "FK_Vendor_CategoryMaster_CategoryMasterUid",
                        column: x => x.CategoryMasterUid,
                        principalTable: "CategoryMaster",
                        principalColumn: "UId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_VendorUId",
                table: "Transaction",
                column: "VendorUId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_CategoryMasterUid",
                table: "Vendor",
                column: "CategoryMasterUid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Vendor_VendorUId",
                table: "Transaction",
                column: "VendorUId",
                principalTable: "Vendor",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Vendor_VendorUId",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_VendorUId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryUid",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "VendorUId",
                table: "Transaction");

            migrationBuilder.AddColumn<short>(
                name: "CategoryUId",
                table: "Transaction",
                type: "smallint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryUId",
                table: "Transaction",
                column: "CategoryUId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_CategoryMaster_CategoryUId",
                table: "Transaction",
                column: "CategoryUId",
                principalTable: "CategoryMaster",
                principalColumn: "UId");
        }
    }
}
