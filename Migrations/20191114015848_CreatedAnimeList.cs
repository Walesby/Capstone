using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class CreatedAnimeList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AnimeList_AnimeItemId",
                table: "AnimeList",
                column: "AnimeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeList_UserId",
                table: "AnimeList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeList_AnimeItem_AnimeItemId",
                table: "AnimeList",
                column: "AnimeItemId",
                principalTable: "AnimeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimeList_AspNetUsers_UserId",
                table: "AnimeList",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimeList_AnimeItem_AnimeItemId",
                table: "AnimeList");

            migrationBuilder.DropForeignKey(
                name: "FK_AnimeList_AspNetUsers_UserId",
                table: "AnimeList");

            migrationBuilder.DropIndex(
                name: "IX_AnimeList_AnimeItemId",
                table: "AnimeList");

            migrationBuilder.DropIndex(
                name: "IX_AnimeList_UserId",
                table: "AnimeList");
        }
    }
}
