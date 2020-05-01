using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeTogether.Infrastructure.Migrations
{
    public partial class detaildelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RisksAndChallenges",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartBudget",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Projects",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Background",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "RisksAndChallenges",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartBudget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Projects");
        }
    }
}
