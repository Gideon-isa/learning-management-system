using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Enrollment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateCourseTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "AvailableCourses",
                newName: "CourseTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseTitle",
                table: "AvailableCourses",
                newName: "Title");
        }
    }
}
