using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppointmentID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AppointmentDateTime = table.Column<DateTime>(nullable: false),
                    Symptoms = table.Column<string>(nullable: false),
                    Diagnosis = table.Column<string>(nullable: true),
                    PatientFullName = table.Column<string>(nullable: false),
                    AppointmentMedicalProfessional = table.Column<string>(nullable: false),
                    UserReferralID = table.Column<int>(nullable: false),
                    AppointmentType = table.Column<string>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    CurrentDate = table.Column<DateTime>(nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppointmentID);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetails",
                columns: table => new
                {
                    ContactDetailsID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(nullable: true),
                    HomePhone = table.Column<string>(nullable: true),
                    WorkPhone = table.Column<string>(nullable: true),
                    MobilePhone = table.Column<string>(nullable: true),
                    NextOfKin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetails", x => x.ContactDetailsID);
                });

            migrationBuilder.CreateTable(
                name: "PatientAppointmentReferral",
                columns: table => new
                {
                    PatientApplicationReferralID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Symptoms = table.Column<string>(nullable: false),
                    Decision = table.Column<string>(nullable: true),
                    RequestedTime = table.Column<string>(nullable: false),
                    RequestedDate = table.Column<DateTime>(nullable: false),
                    DOB = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CurrentDate = table.Column<DateTime>(nullable: false),
                    StreetNumber = table.Column<int>(nullable: false),
                    StreetName = table.Column<string>(nullable: false),
                    County = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: false),
                    Postcode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAppointmentReferral", x => x.PatientApplicationReferralID);
                });

            migrationBuilder.CreateTable(
                name: "PracticeInfo",
                columns: table => new
                {
                    PracticeInfoID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StreetName = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<int>(nullable: false),
                    Postcode = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeInfo", x => x.PracticeInfoID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "ContactDetails");

            migrationBuilder.DropTable(
                name: "PatientAppointmentReferral");

            migrationBuilder.DropTable(
                name: "PracticeInfo");
        }
    }
}
