using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lms.ContentDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedStudentAccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CourseId",
                table: "StudentAccesses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "GrantedAt",
                table: "StudentAccesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsRevoked",
                table: "StudentAccesses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                table: "StudentAccesses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "StudentAccesses");

            migrationBuilder.DropColumn(
                name: "GrantedAt",
                table: "StudentAccesses");

            migrationBuilder.DropColumn(
                name: "IsRevoked",
                table: "StudentAccesses");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                table: "StudentAccesses");
        }
    }
}
