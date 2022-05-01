using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class RemovedProfileUserIdFromTableMatches : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Profiles_fromProfileId",
                table: "Matches",
                column: "fromProfileId",
                principalTable: "Profiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Profiles_fromProfileId",
                table: "Matches");

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
    }
}
