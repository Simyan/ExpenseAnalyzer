using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    public partial class UpdateVendor_NameAndFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_CategoryMaster_CategoryMasterUid",
                table: "Vendor");

            migrationBuilder.RenameColumn(
                name: "CategoryMasterUid",
                table: "Vendor",
                newName: "CategoryMasterUId");

            migrationBuilder.RenameIndex(
                name: "IX_Vendor_CategoryMasterUid",
                table: "Vendor",
                newName: "IX_Vendor_CategoryMasterUId");

            migrationBuilder.AlterColumn<short>(
                name: "CategoryMasterUId",
                table: "Vendor",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_CategoryMaster_CategoryMasterUId",
                table: "Vendor",
                column: "CategoryMasterUId",
                principalTable: "CategoryMaster",
                principalColumn: "UId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendor_CategoryMaster_CategoryMasterUId",
                table: "Vendor");

            migrationBuilder.RenameColumn(
                name: "CategoryMasterUId",
                table: "Vendor",
                newName: "CategoryMasterUid");

            migrationBuilder.RenameIndex(
                name: "IX_Vendor_CategoryMasterUId",
                table: "Vendor",
                newName: "IX_Vendor_CategoryMasterUid");

            migrationBuilder.AlterColumn<short>(
                name: "CategoryMasterUid",
                table: "Vendor",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vendor_CategoryMaster_CategoryMasterUid",
                table: "Vendor",
                column: "CategoryMasterUid",
                principalTable: "CategoryMaster",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
