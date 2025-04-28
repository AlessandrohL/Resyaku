using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resyaku.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixRowGuidIdentityEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "RowGuid",
                table: "ApplicationUser",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "RowGuid",
                table: "ApplicationRole",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_RowGuid",
                table: "ApplicationUser",
                column: "RowGuid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRole_RowGuid",
                table: "ApplicationRole",
                column: "RowGuid",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ApplicationUser_RowGuid",
                table: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationRole_RowGuid",
                table: "ApplicationRole");

            migrationBuilder.DropColumn(
                name: "RowGuid",
                table: "ApplicationRole");

            migrationBuilder.AlterColumn<string>(
                name: "RowGuid",
                table: "ApplicationUser",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }
    }
}
