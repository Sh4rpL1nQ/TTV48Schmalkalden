using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addlocationtocalendartask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "CalendarTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "CalendarTasks");
        }
    }
}
