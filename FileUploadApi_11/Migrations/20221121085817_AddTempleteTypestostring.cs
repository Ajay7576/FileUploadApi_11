using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadApi11.Migrations
{
    /// <inheritdoc />
    public partial class AddTempleteTypestostring : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TempleteTypes",
                table: "BatchUploadDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TempleteTypes",
                table: "BatchUploadDetails",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
