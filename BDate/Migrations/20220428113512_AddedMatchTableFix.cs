using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class AddedMatchTableFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Matches_MatchfromProfileId",
                table: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_MatchfromProfileId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "MatchfromProfileId",
                table: "Profiles");

            migrationBuilder.AddColumn<string>(
                name: "ProfileUserId",
                table: "Matches",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ProfileUserId",
                table: "Matches",
                column: "ProfileUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Profiles_ProfileUserId",
                table: "Matches",
                column: "ProfileUserId",
                principalTable: "Profiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Profiles_ProfileUserId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ProfileUserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ProfileUserId",
                table: "Matches");

            migrationBuilder.AddColumn<string>(
                name: "MatchfromProfileId",
                table: "Profiles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_MatchfromProfileId",
                table: "Profiles",
                column: "MatchfromProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profiles_Matches_MatchfromProfileId",
                table: "Profiles",
                column: "MatchfromProfileId",
                principalTable: "Matches",
                principalColumn: "fromProfileId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
