using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ComeTogether.Infrastructure.Migrations
{
    public partial class t : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ballot_Poll_PollId",
                table: "Ballot");

            migrationBuilder.DropForeignKey(
                name: "FK_Choice_Poll_PollId",
                table: "Choice");

            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Poll_PollId1",
                table: "PollOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Choice_ChoiceId",
                table: "Vote");

            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Poll_PollId",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vote",
                table: "Vote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Poll",
                table: "Poll");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choice",
                table: "Choice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ballot",
                table: "Ballot");

            migrationBuilder.RenameTable(
                name: "Vote",
                newName: "Votes");

            migrationBuilder.RenameTable(
                name: "Poll",
                newName: "Polls");

            migrationBuilder.RenameTable(
                name: "Choice",
                newName: "Choices");

            migrationBuilder.RenameTable(
                name: "Ballot",
                newName: "Ballots");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_PollId",
                table: "Votes",
                newName: "IX_Votes_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_ChoiceId",
                table: "Votes",
                newName: "IX_Votes_ChoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Vote_BallotId",
                table: "Votes",
                newName: "IX_Votes_BallotId");

            migrationBuilder.RenameIndex(
                name: "IX_Choice_PollId",
                table: "Choices",
                newName: "IX_Choices_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Ballot_PollId",
                table: "Ballots",
                newName: "IX_Ballots_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Votes",
                table: "Votes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Polls",
                table: "Polls",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choices",
                table: "Choices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ballots",
                table: "Ballots",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Metrics",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimestampUtc = table.Column<DateTime>(nullable: false),
                    MetricType = table.Column<int>(nullable: false),
                    PollId = table.Column<Guid>(nullable: false),
                    StatusCode = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true),
                    Detail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metrics", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Ballots_Polls_PollId",
                table: "Ballots",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Choices_Polls_PollId",
                table: "Choices",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Polls_PollId1",
                table: "PollOptions",
                column: "PollId1",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Ballots_BallotId",
                table: "Votes",
                column: "BallotId",
                principalTable: "Ballots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Choices_ChoiceId",
                table: "Votes",
                column: "ChoiceId",
                principalTable: "Choices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Polls_PollId",
                table: "Votes",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ballots_Polls_PollId",
                table: "Ballots");

            migrationBuilder.DropForeignKey(
                name: "FK_Choices_Polls_PollId",
                table: "Choices");

            migrationBuilder.DropForeignKey(
                name: "FK_PollOptions_Polls_PollId1",
                table: "PollOptions");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Ballots_BallotId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Choices_ChoiceId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Polls_PollId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "Metrics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Votes",
                table: "Votes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Polls",
                table: "Polls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choices",
                table: "Choices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ballots",
                table: "Ballots");

            migrationBuilder.RenameTable(
                name: "Votes",
                newName: "Vote");

            migrationBuilder.RenameTable(
                name: "Polls",
                newName: "Poll");

            migrationBuilder.RenameTable(
                name: "Choices",
                newName: "Choice");

            migrationBuilder.RenameTable(
                name: "Ballots",
                newName: "Ballot");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_PollId",
                table: "Vote",
                newName: "IX_Vote_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_ChoiceId",
                table: "Vote",
                newName: "IX_Vote_ChoiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_BallotId",
                table: "Vote",
                newName: "IX_Vote_BallotId");

            migrationBuilder.RenameIndex(
                name: "IX_Choices_PollId",
                table: "Choice",
                newName: "IX_Choice_PollId");

            migrationBuilder.RenameIndex(
                name: "IX_Ballots_PollId",
                table: "Ballot",
                newName: "IX_Ballot_PollId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vote",
                table: "Vote",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Poll",
                table: "Poll",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choice",
                table: "Choice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ballot",
                table: "Ballot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ballot_Poll_PollId",
                table: "Ballot",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Choice_Poll_PollId",
                table: "Choice",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PollOptions_Poll_PollId1",
                table: "PollOptions",
                column: "PollId1",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Ballot_BallotId",
                table: "Vote",
                column: "BallotId",
                principalTable: "Ballot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Choice_ChoiceId",
                table: "Vote",
                column: "ChoiceId",
                principalTable: "Choice",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Poll_PollId",
                table: "Vote",
                column: "PollId",
                principalTable: "Poll",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
