using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.CourseManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class outboxremovedcoursecontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseOutboxMessages");

            migrationBuilder.AddColumn<Guid>(
                name: "VideoId",
                table: "LessonVideo",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "VideoMetadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileSize = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoMetadata", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VideoMetadata");

            migrationBuilder.DropColumn(
                name: "VideoId",
                table: "LessonVideo");

            migrationBuilder.CreateTable(
                name: "CourseOutboxMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NextRetryOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OccuredOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RetryCount = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseOutboxMessages", x => x.Id);
                });
        }
    }
}
