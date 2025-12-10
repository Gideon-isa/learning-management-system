using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.ContentDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "CourseContentDelivery",
                newName: "CourseCategoryCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseCategoryCode",
                table: "CourseContentDelivery",
                newName: "Category");
        }
    }
}
