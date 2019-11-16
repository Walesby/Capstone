using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class UpdatedAnimeListModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Fiex",
                table: "AnimeList",
                newName: "UserProgress");

            migrationBuilder.AddColumn<int>(
                name: "CompleteStatus",
                table: "AnimeList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompleteStatus",
                table: "AnimeList");

            migrationBuilder.RenameColumn(
                name: "UserProgress",
                table: "AnimeList",
                newName: "Fiex");
        }
    }
}
