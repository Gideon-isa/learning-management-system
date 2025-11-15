using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.ContentDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedVideoId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "StudentAccesses",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "LessonVideoContent",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccesses_StudentCode_CourseId",
                table: "StudentAccesses",
                columns: new[] { "StudentCode", "CourseId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StudentAccesses_StudentCode_CourseId",
                table: "StudentAccesses");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "LessonVideoContent");

            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "StudentAccesses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
