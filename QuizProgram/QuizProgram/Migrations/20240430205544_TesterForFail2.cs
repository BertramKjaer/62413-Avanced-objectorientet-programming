using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizProgram.Migrations
{
    /// <inheritdoc />
    public partial class TesterForFail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: "62413");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "Name" },
                values: new object[] { "62413", "Advanced object oriented programming using C# and .NET" });
        }
    }
}
