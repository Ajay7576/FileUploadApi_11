using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadApi11.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "BatchUploadDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "BatchUploadDetails");
        }
    }
}
