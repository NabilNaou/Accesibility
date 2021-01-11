using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetTalk.Migrations
{
    public partial class viewed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_Post_PostId1",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_PostId1",
                table: "View");

            migrationBuilder.DropColumn(
                name: "PostId1",
                table: "View");

            migrationBuilder.AlterColumn<int>(
                name: "PostId",
                table: "View",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_View_PostId",
                table: "View",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_View_Post_PostId",
                table: "View",
                column: "PostId",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_View_Post_PostId",
                table: "View");

            migrationBuilder.DropIndex(
                name: "IX_View_PostId",
                table: "View");

            migrationBuilder.AlterColumn<string>(
                name: "PostId",
                table: "View",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PostId1",
                table: "View",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_View_PostId1",
                table: "View",
                column: "PostId1");

            migrationBuilder.AddForeignKey(
                name: "FK_View_Post_PostId1",
                table: "View",
                column: "PostId1",
                principalTable: "Post",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
