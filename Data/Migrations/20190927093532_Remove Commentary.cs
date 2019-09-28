using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class RemoveCommentary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentaryId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentaryId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentaryId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentaryId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentaryId",
                table: "Comments",
                column: "CommentaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentaryId",
                table: "Comments",
                column: "CommentaryId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
