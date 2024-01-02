using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todoist.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Categories_CategoryID",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "Goals",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_CategoryID",
                table: "Goals",
                newName: "IX_Goals_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Categories_CategoryId",
                table: "Goals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goals_Categories_CategoryId",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Goals",
                newName: "CategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Goals_CategoryId",
                table: "Goals",
                newName: "IX_Goals_CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Goals_Categories_CategoryID",
                table: "Goals",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
