using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoColaboradoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckInTime",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "CheckOutTime",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Attendances",
                newName: "ExitTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "Attendances",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "Attendances");

            migrationBuilder.RenameColumn(
                name: "ExitTime",
                table: "Attendances",
                newName: "Date");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckInTime",
                table: "Attendances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "CheckOutTime",
                table: "Attendances",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
