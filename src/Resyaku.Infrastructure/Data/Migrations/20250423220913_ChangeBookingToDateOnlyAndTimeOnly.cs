using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resyaku.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeBookingToDateOnlyAndTimeOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Booking",
                newName: "DurationMinutes");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndTime",
                table: "Booking",
                type: "time",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BookingDate",
                table: "Booking",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DurationMinutes",
                table: "Booking",
                newName: "Duration");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndTime",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time");

            migrationBuilder.AlterColumn<DateTime>(
                name: "BookingDate",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
