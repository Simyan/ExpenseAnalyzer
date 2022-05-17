using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseAnalyzer.Migrations
{
    public partial class AddFKRelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CategoryUId",
                table: "Transaction",
                column: "CategoryUId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TypeUId",
                table: "Transaction",
                column: "TypeUId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_CategoryMaster_CategoryUId",
                table: "Transaction",
                column: "CategoryUId",
                principalTable: "CategoryMaster",
                principalColumn: "UId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_TypeMaster_TypeUId",
                table: "Transaction",
                column: "TypeUId",
                principalTable: "TypeMaster",
                principalColumn: "UId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_CategoryMaster_CategoryUId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_TypeMaster_TypeUId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_CategoryUId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TypeUId",
                table: "Transaction");
        }
    }
}
