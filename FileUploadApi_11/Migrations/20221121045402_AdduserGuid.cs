using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FileUploadApi11.Migrations
{
    /// <inheritdoc />
    public partial class AdduserGuid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "BatchUploadDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "BatchUploadDetails",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BatchUploadDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "BatchUploadDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
