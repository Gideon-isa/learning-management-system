using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.Enrollment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedCourseTitle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CourseTitle",
                table: "CourseEnrollments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseTitle",
                table: "CourseEnrollments");
        }
    }
}
