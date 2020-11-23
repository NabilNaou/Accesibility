using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StreetTalk.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    filename = table.Column<string>(type: "TEXT", nullable: true),
                    sensitive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    emailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    passwordHash = table.Column<string>(type: "TEXT", nullable: true),
                    lockoutEndTime = table.Column<DateTime>(type: "TEXT", nullable: true),
                    lockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    accessFailedCount = table.Column<int>(type: "INTEGER", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    title = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    photoId = table.Column<int>(type: "INTEGER", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    pseudonym = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    closed = table.Column<bool>(type: "INTEGER", nullable: true),
                    reportCount = table.Column<int>(type: "INTEGER", nullable: true),
                    modifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_posts_Photo_photoId",
                        column: x => x.photoId,
                        principalTable: "Photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_posts_Photo_photoId1",
                        column: x => x.photoId,
                        principalTable: "Photo",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Profile",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    firstName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    lastName = table.Column<string>(type: "TEXT", maxLength: 45, nullable: true),
                    dateOfBirth = table.Column<DateTime>(type: "Date", nullable: true),
                    city = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    street = table.Column<string>(type: "TEXT", maxLength: 64, nullable: true),
                    houseNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    houseNumberAddition = table.Column<string>(type: "TEXT", maxLength: 5, nullable: true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profile", x => x.id);
                    table.ForeignKey(
                        name: "FK_Profile_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    content = table.Column<string>(type: "TEXT", maxLength: 600, nullable: true),
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    postId = table.Column<int>(type: "INTEGER", nullable: false),
                    modifiedAt = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.id);
                    table.ForeignKey(
                        name: "FK_Comment_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    userId = table.Column<int>(type: "INTEGER", nullable: false),
                    postId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => new { x.userId, x.postId });
                    table.ForeignKey(
                        name: "FK_Like_posts_postId",
                        column: x => x.postId,
                        principalTable: "posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Like_users_userId",
                        column: x => x.userId,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comment_postId",
                table: "Comment",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_userId",
                table: "Comment",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Like_postId",
                table: "Like",
                column: "postId");

            migrationBuilder.CreateIndex(
                name: "IX_posts_photoId",
                table: "posts",
                column: "photoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_posts_photoId1",
                table: "posts",
                column: "photoId");

            migrationBuilder.CreateIndex(
                name: "IX_Profile_userId",
                table: "Profile",
                column: "userId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Like");

            migrationBuilder.DropTable(
                name: "Profile");

            migrationBuilder.DropTable(
                name: "posts");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Photo");
        }
    }
}
