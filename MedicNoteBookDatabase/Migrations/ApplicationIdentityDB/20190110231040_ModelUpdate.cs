using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations.ApplicationIdentityDB
{
    public partial class ModelUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckPassword",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Account",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Account",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "AddressID",
                table: "Account",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContactID",
                table: "Account",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "ContactID",
                table: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Account",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Account",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CheckPassword",
                table: "Account",
                nullable: true);
        }
    }
}
