using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addhovercodeandisrecurrentflagtocalendartask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoverCode",
                table: "CalendarTaskType",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecurrent",
                table: "CalendarTasks",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoverCode",
                table: "CalendarTaskType");

            migrationBuilder.DropColumn(
                name: "IsRecurrent",
                table: "CalendarTasks");
        }
    }
}
