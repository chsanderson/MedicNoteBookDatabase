using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations
{
    public partial class Times : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppointmentDateTime",
                table: "Appointment",
                newName: "AppointmentDate");

            migrationBuilder.AddColumn<string>(
                name: "MedicalPersonnel",
                table: "PatientAppointmentReferral",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppointmentTime",
                table: "Appointment",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MedicalPersonnel",
                table: "Account",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropColumn(
                name: "MedicalPersonnel",
                table: "PatientAppointmentReferral");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "MedicalPersonnel",
                table: "Account");

            migrationBuilder.RenameColumn(
                name: "AppointmentDate",
                table: "Appointment",
                newName: "AppointmentDateTime");
        }
    }
}
