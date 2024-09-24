using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizyZunaAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TimesAnsweredAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Answers_CorrectAnswer",
                table: "Questions",
                newName: "Answers_CorrectAnswer_Value");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .OldAnnotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas");

            migrationBuilder.AddColumn<int>(
                name: "TimesAnswered",
                table: "WrongAnswer",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Answers_CorrectAnswer_TimesAnswered",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesAnswered",
                table: "WrongAnswer");

            migrationBuilder.DropColumn(
                name: "Answers_CorrectAnswer_TimesAnswered",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Answers_CorrectAnswer_Value",
                table: "Questions",
                newName: "Answers_CorrectAnswer");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:difficulty", "beginner,novice,intermediate,difficult,expert")
                .Annotation("Npgsql:Enum:topic", "geography,history,sciences,literature,cinema,video_games,music,visual_arts,technology,living_beings,mythology,television,sport,gastronomy,cartoons,architecture,mangas");
        }
    }
}
