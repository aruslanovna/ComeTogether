using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeTogether.Infrastructure.Migrations
{
    public partial class proj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AOV",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ARPU",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CAC",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPO",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LTV",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROAS",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROI",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROMI",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AOV",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ARPU",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CAC",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CPO",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LTV",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROAS",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROI",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ROMI",
                table: "ProjectDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AOV",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ARPU",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CAC",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CPO",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LTV",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ROAS",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ROI",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ROMI",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AOV",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ARPU",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "CAC",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "CPO",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "LTV",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ROAS",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ROI",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "ROMI",
                table: "ProjectDetails");
        }
    }
}
