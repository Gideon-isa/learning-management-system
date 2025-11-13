using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.ContentDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDbTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleContents_CourseContents_CourseId",
                table: "CourseModuleContents");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonContents_CourseModuleContents_ModuleId",
                table: "LessonContents");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonVideoContent_LessonContents_LessonId",
                table: "LessonVideoContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonContents",
                table: "LessonContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleContents",
                table: "CourseModuleContents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContents",
                table: "CourseContents");

            migrationBuilder.RenameTable(
                name: "LessonContents",
                newName: "LessonContentDelivery");

            migrationBuilder.RenameTable(
                name: "CourseModuleContents",
                newName: "CourseModuleContentDelivery");

            migrationBuilder.RenameTable(
                name: "CourseContents",
                newName: "CourseContentDelivery");

            migrationBuilder.RenameIndex(
                name: "IX_LessonContents_ModuleId",
                table: "LessonContentDelivery",
                newName: "IX_LessonContentDelivery_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleContents_CourseId",
                table: "CourseModuleContentDelivery",
                newName: "IX_CourseModuleContentDelivery_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContents_CourseTitle_InstructorId",
                table: "CourseContentDelivery",
                newName: "IX_CourseContentDelivery_CourseTitle_InstructorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonContentDelivery",
                table: "LessonContentDelivery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleContentDelivery",
                table: "CourseModuleContentDelivery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContentDelivery",
                table: "CourseContentDelivery",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleContentDelivery_CourseContentDelivery_CourseId",
                table: "CourseModuleContentDelivery",
                column: "CourseId",
                principalTable: "CourseContentDelivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonContentDelivery_CourseModuleContentDelivery_ModuleId",
                table: "LessonContentDelivery",
                column: "ModuleId",
                principalTable: "CourseModuleContentDelivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonVideoContent_LessonContentDelivery_LessonId",
                table: "LessonVideoContent",
                column: "LessonId",
                principalTable: "LessonContentDelivery",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseModuleContentDelivery_CourseContentDelivery_CourseId",
                table: "CourseModuleContentDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonContentDelivery_CourseModuleContentDelivery_ModuleId",
                table: "LessonContentDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonVideoContent_LessonContentDelivery_LessonId",
                table: "LessonVideoContent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonContentDelivery",
                table: "LessonContentDelivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseModuleContentDelivery",
                table: "CourseModuleContentDelivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourseContentDelivery",
                table: "CourseContentDelivery");

            migrationBuilder.RenameTable(
                name: "LessonContentDelivery",
                newName: "LessonContents");

            migrationBuilder.RenameTable(
                name: "CourseModuleContentDelivery",
                newName: "CourseModuleContents");

            migrationBuilder.RenameTable(
                name: "CourseContentDelivery",
                newName: "CourseContents");

            migrationBuilder.RenameIndex(
                name: "IX_LessonContentDelivery_ModuleId",
                table: "LessonContents",
                newName: "IX_LessonContents_ModuleId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseModuleContentDelivery_CourseId",
                table: "CourseModuleContents",
                newName: "IX_CourseModuleContents_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_CourseContentDelivery_CourseTitle_InstructorId",
                table: "CourseContents",
                newName: "IX_CourseContents_CourseTitle_InstructorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonContents",
                table: "LessonContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseModuleContents",
                table: "CourseModuleContents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourseContents",
                table: "CourseContents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseModuleContents_CourseContents_CourseId",
                table: "CourseModuleContents",
                column: "CourseId",
                principalTable: "CourseContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonContents_CourseModuleContents_ModuleId",
                table: "LessonContents",
                column: "ModuleId",
                principalTable: "CourseModuleContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonVideoContent_LessonContents_LessonId",
                table: "LessonVideoContent",
                column: "LessonId",
                principalTable: "LessonContents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
