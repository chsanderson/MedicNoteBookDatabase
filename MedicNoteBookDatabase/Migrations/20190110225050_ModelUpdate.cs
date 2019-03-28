using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HomePhone",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobilePhone",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NextOfKin",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkPhone",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Postcode",
                table: "Address",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "PatientAppointmentReferral");

            migrationBuilder.DropColumn(
                name: "HomePhone",
                table: "PatientAppointmentReferral");

            migrationBuilder.DropColumn(
                name: "MobilePhone",
                table: "PatientAppointmentReferral");

            migrationBuilder.DropColumn(
                name: "NextOfKin",
                table: "PatientAppointmentReferral");

            migrationBuilder.DropColumn(
                name: "WorkPhone",
                table: "PatientAppointmentReferral");

            migrationBuilder.AlterColumn<string>(
                name: "Postcode",
                table: "Address",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 9);
        }
    }
}
