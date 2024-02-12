using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todoist.Migrations
{
    /// <inheritdoc />
    public partial class RoolBackExampleNotNULL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "testField",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "testField2",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "NameCategory",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NameCategory",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "testField",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "testField2",
                table: "Categories",
                type: "int",
                nullable: true);
        }
    }
}
