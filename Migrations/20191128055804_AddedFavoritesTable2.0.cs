using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class AddedFavoritesTable20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Favorites",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    AnimeItemId = table.Column<int>(nullable: false),
                    MangaItemId = table.Column<int>(nullable: false),
                    NovelItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favorites_AnimeItem_AnimeItemId",
                        column: x => x.AnimeItemId,
                        principalTable: "AnimeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_MangaItem_MangaItemId",
                        column: x => x.MangaItemId,
                        principalTable: "MangaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favorites_NovelItem_NovelItemId",
                        column: x => x.NovelItemId,
                        principalTable: "NovelItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_AnimeItemId",
                table: "Favorites",
                column: "AnimeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_MangaItemId",
                table: "Favorites",
                column: "MangaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorites_NovelItemId",
                table: "Favorites",
                column: "NovelItemId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favorites");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");
        }
    }
}
