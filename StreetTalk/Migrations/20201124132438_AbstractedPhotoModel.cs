using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetTalk.Migrations
{
    public partial class AbstractedPhotoModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Photo_PhotoId",
                table: "Post");

            migrationBuilder.DropIndex(
                name: "IX_Post_PhotoId",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Sensitive",
                table: "Photo");

            migrationBuilder.CreateTable(
                name: "PostPhoto",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    PostId = table.Column<int>(type: "INTEGER", nullable: false),
                    Sensitive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostPhoto", x => new { x.PhotoId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostPhoto_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostPhoto_Post_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProfilePhoto",
                columns: table => new
                {
                    PhotoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProfileId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilePhoto", x => new { x.PhotoId, x.ProfileId });
                    table.ForeignKey(
                        name: "FK_ProfilePhoto_Photo_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "Photo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilePhoto_Profile_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostPhoto_PostId",
                table: "PostPhoto",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProfilePhoto_ProfileId",
                table: "ProfilePhoto",
                column: "ProfileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostPhoto");

            migrationBuilder.DropTable(
                name: "ProfilePhoto");

            migrationBuilder.AddColumn<bool>(
                name: "Sensitive",
                table: "Photo",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Post_PhotoId",
                table: "Post",
                column: "PhotoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Photo_PhotoId",
                table: "Post",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
