using Microsoft.EntityFrameworkCore.Migrations;

namespace PrTask.DAL.Migrations
{
    public partial class updateUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserEnt",
                table: "UserEnt");

            migrationBuilder.RenameTable(
                name: "UserEnt",
                newName: "Users");

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserEnt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserEnt",
                table: "UserEnt",
                column: "Id");
        }
    }
}
