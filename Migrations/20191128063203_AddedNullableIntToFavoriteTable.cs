using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class AddedNullableIntToFavoriteTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AnimeItem_AnimeItemId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_MangaItem_MangaItemId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_NovelItem_NovelItemId",
                table: "Favorites");

            migrationBuilder.AlterColumn<int>(
                name: "NovelItemId",
                table: "Favorites",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "MangaItemId",
                table: "Favorites",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AnimeItemId",
                table: "Favorites",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AnimeItem_AnimeItemId",
                table: "Favorites",
                column: "AnimeItemId",
                principalTable: "AnimeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_MangaItem_MangaItemId",
                table: "Favorites",
                column: "MangaItemId",
                principalTable: "MangaItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_NovelItem_NovelItemId",
                table: "Favorites",
                column: "NovelItemId",
                principalTable: "NovelItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_AnimeItem_AnimeItemId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_MangaItem_MangaItemId",
                table: "Favorites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favorites_NovelItem_NovelItemId",
                table: "Favorites");

            migrationBuilder.AlterColumn<int>(
                name: "NovelItemId",
                table: "Favorites",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MangaItemId",
                table: "Favorites",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimeItemId",
                table: "Favorites",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_AnimeItem_AnimeItemId",
                table: "Favorites",
                column: "AnimeItemId",
                principalTable: "AnimeItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_MangaItem_MangaItemId",
                table: "Favorites",
                column: "MangaItemId",
                principalTable: "MangaItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favorites_NovelItem_NovelItemId",
                table: "Favorites",
                column: "NovelItemId",
                principalTable: "NovelItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
