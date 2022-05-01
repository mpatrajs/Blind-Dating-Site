using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class AddedMatchTableStructureUpdateAndFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Profiles_matchedProfileId",
                table: "Matches");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "matchedProfileId",
                table: "Matches",
                newName: "toProfileId");

            migrationBuilder.AddColumn<string>(
                name: "fromProfileId",
                table: "Matches",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProfileUserId",
                table: "Matches",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                columns: new[] { "fromProfileId", "toProfileId" });

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Matches",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ProfileUserId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "fromProfileId",
                table: "Matches");

            migrationBuilder.DropColumn(
                name: "ProfileUserId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "toProfileId",
                table: "Matches",
                newName: "matchedProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Matches",
                table: "Matches",
                column: "matchedProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Profiles_matchedProfileId",
                table: "Matches",
                column: "matchedProfileId",
                principalTable: "Profiles",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
