using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetTalk.Migrations
{
    public partial class RemovedPhotoIdFromPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "Post",
                type: "INTEGER",
                nullable: true);
        }
    }
}
