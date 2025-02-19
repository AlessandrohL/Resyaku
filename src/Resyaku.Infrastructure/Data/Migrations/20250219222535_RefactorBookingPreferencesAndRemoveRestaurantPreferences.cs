using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resyaku.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefactorBookingPreferencesAndRemoveRestaurantPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RestaurantPreferences");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DailyClosingTime",
                table: "BookingPreferences",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DailyOpeningTime",
                table: "BookingPreferences",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyClosingTime",
                table: "BookingPreferences");

            migrationBuilder.DropColumn(
                name: "DailyOpeningTime",
                table: "BookingPreferences");

            migrationBuilder.CreateTable(
                name: "RestaurantPreferences",
                columns: table => new
                {
                    RestaurantPrefId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClosingTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsSpecial = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    RowUlid = table.Column<string>(type: "nvarchar(26)", maxLength: 26, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantPreferences", x => x.RestaurantPrefId);
                    table.CheckConstraint("CK_RestaurantPreferences_IsSpecial", "[IsSpecial] = 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantPreferences_IsSpecial",
                table: "RestaurantPreferences",
                column: "IsSpecial",
                unique: true);
        }
    }
}
