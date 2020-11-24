using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetTalk.Migrations
{
    public partial class UserPostRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_Photo_photoId1",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_photoId1",
                table: "posts");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "posts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_posts_userId",
                table: "posts",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_userId",
                table: "posts",
                column: "userId",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_userId",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_userId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "posts");

            migrationBuilder.CreateIndex(
                name: "IX_posts_photoId1",
                table: "posts",
                column: "photoId");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_Photo_photoId1",
                table: "posts",
                column: "photoId",
                principalTable: "Photo",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
