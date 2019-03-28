using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations
{
    public partial class TimeString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Time",
                table: "Times",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Time",
                table: "Times",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
