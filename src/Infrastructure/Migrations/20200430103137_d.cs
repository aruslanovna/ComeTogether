using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeTogether.Infrastructure.Migrations
{
    public partial class d : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropIndex(
                name: "IX_PollOptions_PollId",
                table: "PollOptions");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "ProjectDetails");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "ProjectDetails");

            migrationBuilder.AddColumn<long>(
                name: "PollId1",
                table: "PollOptions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UUID = table.Column<Guid>(nullable: false),
                    ManageId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Creator = table.Column<string>(nullable: true),
                    PollType = table.Column<int>(nullable: false),
                    CreatorIdentity = table.Column<string>(nullable: true),
                    MaxPoints = table.Column<int>(nullable: true),
                    MaxPerVote = table.Column<int>(nullable: true),
                    InviteOnly = table.Column<bool>(nullable: false),
                    NamedVoting = table.Column<bool>(nullable: false),
                    ExpiryDateUtc = table.Column<DateTime>(nullable: true),
                    ChoiceAdding = table.Column<bool>(nullable: false),
                    LastUpdatedUtc = table.Column<DateTime>(nullable: false),
                    CreatedDateUtc = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ballot",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManageGuid = table.Column<Guid>(nullable: false),
                    TokenGuid = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    VoterName = table.Column<string>(nullable: true),
                    HasVoted = table.Column<bool>(nullable: false),
                    PollId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ballot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ballot_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PollChoiceNumber = table.Column<int>(nullable: false),
                    PollId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Choice_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Vote",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoiceId = table.Column<long>(nullable: true),
                    VoteValue = table.Column<int>(nullable: false),
                    BallotId = table.Column<long>(nullable: true),
                    PollId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vote_Ballot_BallotId",
                        column: x => x.BallotId,
                        principalTable: "Ballot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_Choice_ChoiceId",
                        column: x => x.ChoiceId,
                        principalTable: "Choice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Vote_Poll_PollId",
                        column: x => x.PollId,
                        principalTable: "Poll",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollId1",
                table: "PollOptions",
                column: "PollId1");

            migrationBuilder.CreateIndex(
                name: "IX_Ballot_PollId",
                table: "Ballot",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Choice_PollId",
                table: "Choice",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_BallotId",
                table: "Vote",
                column: "BallotId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_ChoiceId",
                table: "Vote",
                column: "ChoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Vote_PollId",
                table: "Vote",
                column: "PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Poll_PollId1",
                table: "PollOptions",
                column: "PollId1",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Poll_PollId1",
                table: "PollOptions");

            migrationBuilder.DropTable(
                name: "Vote");

            migrationBuilder.DropTable(
                name: "Ballot");

            migrationBuilder.DropTable(
                name: "Choice");

            migrationBuilder.DropTable(
                name: "Poll");

            migrationBuilder.DropIndex(
                name: "IX_PollOptions_PollId1",
                table: "PollOptions");

            migrationBuilder.DropColumn(
                name: "PollId1",
                table: "PollOptions");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "ProjectDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ProjectDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModified",
                table: "ProjectDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "ProjectDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    PollId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.PollId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PollOptions_PollId",
                table: "PollOptions",
                column: "PollId");

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Polls_PollId",
                table: "PollOptions",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "PollId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
