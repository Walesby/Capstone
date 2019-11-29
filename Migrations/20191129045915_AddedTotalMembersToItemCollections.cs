using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class AddedTotalMembersToItemCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Members",
                table: "NovelItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Members",
                table: "MangaItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Members",
                table: "AnimeItem",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Members",
                table: "NovelItem");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "MangaItem");

            migrationBuilder.DropColumn(
                name: "Members",
                table: "AnimeItem");
        }
    }
}
