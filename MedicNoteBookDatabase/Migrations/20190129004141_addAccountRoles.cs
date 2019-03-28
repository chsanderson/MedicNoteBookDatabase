using Microsoft.EntityFrameworkCore.Migrations;

namespace MedicNoteBookDatabase.Migrations
{
    public partial class addAccountRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleID",
                table: "Account",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleID",
                table: "Account");
        }
    }
}
