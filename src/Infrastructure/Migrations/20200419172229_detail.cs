using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeTogether.Infrastructure.Migrations
{
    public partial class detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId1",
                table: "ProjectDetails");

            migrationBuilder.DropIndex(
                name: "IX_ProjectDetails_ProjectId1",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "ProjectDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProjectDetailsProjectId",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Background",
                table: "ProjectDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullDescription",
                table: "ProjectDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RisksAndChallenges",
                table: "ProjectDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortDescription",
                table: "ProjectDetails",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StartBudget",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ProjectDetailsProjectId",
                table: "Projects",
                column: "ProjectDetailsProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_ProjectDetails_ProjectDetailsProjectId",
                table: "Projects",
                column: "ProjectDetailsProjectId",
                principalTable: "ProjectDetails",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_ProjectDetails_ProjectDetailsProjectId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ProjectDetailsProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectDetailsProjectId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Background",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "FullDescription",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "RisksAndChallenges",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ShortDescription",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "StartBudget",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "ProjectDetails");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId1",
                table: "ProjectDetails",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ProjectId1",
                table: "ProjectDetails",
                column: "ProjectId1");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectDetails_Projects_ProjectId1",
                table: "ProjectDetails",
                column: "ProjectId1",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
