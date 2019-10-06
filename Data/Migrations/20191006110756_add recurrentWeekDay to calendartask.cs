using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addrecurrentWeekDaytocalendartask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRecurrent",
                table: "CalendarTasks");

            migrationBuilder.AddColumn<int>(
                name: "RecurrentWeekDay",
                table: "CalendarTasks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecurrentWeekDay",
                table: "CalendarTasks");

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurrent",
                table: "CalendarTasks",
                nullable: false,
                defaultValue: false);
        }
    }
}
