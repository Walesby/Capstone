using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Capstone.Migrations
{
    public partial class AddedMangaAndNovelTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "Aired",
                table: "NovelItem");

            migrationBuilder.DropColumn(
                name: "EpisodeDuration",
                table: "NovelItem");

            migrationBuilder.DropColumn(
                name: "Aired",
                table: "MangaItem");

            migrationBuilder.DropColumn(
                name: "EpisodeDuration",
                table: "MangaItem");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecommendedAge",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Premiered",
                table: "NovelItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "NovelItem",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RecommendedAge",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Premiered",
                table: "MangaItem",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "MangaItem",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimeReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    AnimeItemId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Review = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimeReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimeReviews_AnimeItem_AnimeItemId",
                        column: x => x.AnimeItemId,
                        principalTable: "AnimeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimeReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    MangaItemId = table.Column<int>(nullable: false),
                    UserRating = table.Column<int>(nullable: false),
                    UserProgress = table.Column<int>(nullable: false),
                    CompleteStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaList_MangaItem_MangaItemId",
                        column: x => x.MangaItemId,
                        principalTable: "MangaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MangaReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    MangaItemId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Review = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MangaReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MangaReviews_MangaItem_MangaItemId",
                        column: x => x.MangaItemId,
                        principalTable: "MangaItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MangaReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NovelList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    NovelItemId = table.Column<int>(nullable: false),
                    UserRating = table.Column<int>(nullable: false),
                    UserProgress = table.Column<int>(nullable: false),
                    CompleteStatus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NovelList_NovelItem_NovelItemId",
                        column: x => x.NovelItemId,
                        principalTable: "NovelItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelList_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NovelReviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostDate = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    NovelItemId = table.Column<int>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Review = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NovelReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NovelReviews_NovelItem_NovelItemId",
                        column: x => x.NovelItemId,
                        principalTable: "NovelItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NovelReviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimeReviews_AnimeItemId",
                table: "AnimeReviews",
                column: "AnimeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimeReviews_UserId",
                table: "AnimeReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaList_MangaItemId",
                table: "MangaList",
                column: "MangaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaList_UserId",
                table: "MangaList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaReviews_MangaItemId",
                table: "MangaReviews",
                column: "MangaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MangaReviews_UserId",
                table: "MangaReviews",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelList_NovelItemId",
                table: "NovelList",
                column: "NovelItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelList_UserId",
                table: "NovelList",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelReviews_NovelItemId",
                table: "NovelReviews",
                column: "NovelItemId");

            migrationBuilder.CreateIndex(
                name: "IX_NovelReviews_UserId",
                table: "NovelReviews",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimeReviews");

            migrationBuilder.DropTable(
                name: "MangaList");

            migrationBuilder.DropTable(
                name: "MangaReviews");

            migrationBuilder.DropTable(
                name: "NovelList");

            migrationBuilder.DropTable(
                name: "NovelReviews");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "NovelItem");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "MangaItem");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RecommendedAge",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Premiered",
                table: "NovelItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "Aired",
                table: "NovelItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EpisodeDuration",
                table: "NovelItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Synopsis",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Source",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RecommendedAge",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Premiered",
                table: "MangaItem",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "Aired",
                table: "MangaItem",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "EpisodeDuration",
                table: "MangaItem",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AnimeItemId = table.Column<int>(nullable: false),
                    PostDate = table.Column<DateTime>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Review = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AnimeItem_AnimeItemId",
                        column: x => x.AnimeItemId,
                        principalTable: "AnimeItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AnimeItemId",
                table: "Reviews",
                column: "AnimeItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }
    }
}
