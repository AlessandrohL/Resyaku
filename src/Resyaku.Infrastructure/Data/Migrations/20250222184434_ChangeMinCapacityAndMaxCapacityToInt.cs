using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resyaku.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMinCapacityAndMaxCapacityToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MinCapacity",
                table: "Table",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<int>(
                name: "MaxCapacity",
                table: "Table",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "MinCapacity",
                table: "Table",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "MaxCapacity",
                table: "Table",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
