using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizProgram.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQuiz : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Quizzes");

            migrationBuilder.RenameColumn(
                name: "Question",
                table: "Quizzes",
                newName: "Title");

            migrationBuilder.AlterColumn<int>(
                name: "QuizId",
                table: "Quizzes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.CreateTable(
                name: "Question",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuizQuestion = table.Column<string>(type: "TEXT", nullable: false),
                    CorrectAnswer = table.Column<string>(type: "TEXT", nullable: false),
                    IncorrectAnswer1 = table.Column<string>(type: "TEXT", nullable: false),
                    IncorrectAnswer2 = table.Column<string>(type: "TEXT", nullable: false),
                    IncorrectAnswer3 = table.Column<string>(type: "TEXT", nullable: false),
                    QuizId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Question", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Question_Quizzes_QuizId",
                        column: x => x.QuizId,
                        principalTable: "Quizzes",
                        principalColumn: "QuizId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Question_QuizId",
                table: "Question",
                column: "QuizId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Question");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Quizzes",
                newName: "Question");

            migrationBuilder.AlterColumn<string>(
                name: "QuizId",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Quizzes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
