using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoColaboradoresBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAttendanceToDateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExitTime",
                table: "Attendances",
                newName: "CheckOutTime");

            migrationBuilder.RenameColumn(
                name: "EntryTime",
                table: "Attendances",
                newName: "CheckInTime");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Collaborators",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Collaborators",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collaborators",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CheckOutTime",
                table: "Attendances",
                newName: "ExitTime");

            migrationBuilder.RenameColumn(
                name: "CheckInTime",
                table: "Attendances",
                newName: "EntryTime");

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "Collaborators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "Collaborators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Collaborators",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
