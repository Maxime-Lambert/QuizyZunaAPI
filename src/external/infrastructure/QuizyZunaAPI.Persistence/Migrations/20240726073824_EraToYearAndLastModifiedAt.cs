using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizyZunaAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EraToYearAndLastModifiedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags_Era",
                table: "Questions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .Annotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas")
                .OldAnnotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .OldAnnotation("Npgsql:Enum:era", "none,prehistory,antiquity,middle_age,modern,nineteenth_century,twentyth_century,twenty_first_century")
                .OldAnnotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedAt",
                table: "Questions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Tags_Year",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifiedAt",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Tags_Year",
                table: "Questions");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .Annotation("Npgsql:Enum:era", "none,prehistory,antiquity,middle_age,modern,nineteenth_century,twentyth_century,twenty_first_century")
                .Annotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas")
                .OldAnnotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .OldAnnotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas");

            migrationBuilder.AddColumn<int>(
                name: "Tags_Era",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
