﻿// <auto-generated />
using System;
using MedicNoteBookDatabase.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MedicNoteBookDatabase.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20190129004141_addAccountRoles")]
    partial class addAccountRoles
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MedicNoteBookDatabase.Models.Account", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressID");

                    b.Property<string>("CHINumber")
                        .HasMaxLength(10);

                    b.Property<int>("ContactID");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Password");

                    b.Property<int>("RoleID");

                    b.Property<string>("Username");

                    b.HasKey("ID");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.Address", b =>
                {
                    b.Property<int>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("County");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasMaxLength(9);

                    b.Property<string>("Region")
                        .IsRequired();

                    b.Property<string>("StreetName")
                        .IsRequired();

                    b.Property<int>("StreetNumber");

                    b.HasKey("AddressID");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AppointmentDateTime");

                    b.Property<string>("AppointmentMedicalProfessional")
                        .IsRequired();

                    b.Property<string>("AppointmentType")
                        .IsRequired();

                    b.Property<string>("County");

                    b.Property<DateTime>("CurrentDate");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Diagnosis");

                    b.Property<string>("PatientFullName")
                        .IsRequired();

                    b.Property<string>("Postcode")
                        .IsRequired();

                    b.Property<string>("Region")
                        .IsRequired();

                    b.Property<string>("StreetName")
                        .IsRequired();

                    b.Property<int>("StreetNumber");

                    b.Property<string>("Symptoms")
                        .IsRequired();

                    b.Property<int>("UserReferralID");

                    b.HasKey("AppointmentID");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.ContactDetails", b =>
                {
                    b.Property<int>("ContactDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("HomePhone");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("NextOfKin");

                    b.Property<string>("WorkPhone");

                    b.HasKey("ContactDetailsID");

                    b.ToTable("ContactDetails");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.PatientAppointmentReferral", b =>
                {
                    b.Property<int>("PatientApplicationReferralID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("County");

                    b.Property<DateTime>("CurrentDate");

                    b.Property<DateTime>("DOB");

                    b.Property<string>("Decision");

                    b.Property<string>("Email");

                    b.Property<string>("HomePhone");

                    b.Property<string>("MobilePhone");

                    b.Property<string>("Name");

                    b.Property<string>("NextOfKin");

                    b.Property<string>("Postcode");

                    b.Property<string>("Region");

                    b.Property<DateTime>("RequestedDate");

                    b.Property<string>("RequestedTime")
                        .IsRequired();

                    b.Property<string>("StreetName");

                    b.Property<int>("StreetNumber");

                    b.Property<string>("Symptoms");

                    b.Property<string>("WorkPhone");

                    b.HasKey("PatientApplicationReferralID");

                    b.ToTable("PatientAppointmentReferral");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.PracticeInfo", b =>
                {
                    b.Property<int>("PracticeInfoID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("County");

                    b.Property<string>("Postcode");

                    b.Property<string>("Region");

                    b.Property<string>("StreetName");

                    b.Property<int>("StreetNumber");

                    b.HasKey("PracticeInfoID");

                    b.ToTable("PracticeInfo");
                });

            modelBuilder.Entity("MedicNoteBookDatabase.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserRole")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });
#pragma warning restore 612, 618
        }
    }
}
