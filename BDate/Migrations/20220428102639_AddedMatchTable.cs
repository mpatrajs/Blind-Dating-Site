using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class AddedMatchTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MatchfromProfileId",
                table: "Profiles",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    fromProfileId = table.Column<string>(type: "text", nullable: false),
                    toProfileId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.fromProfileId);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profiles_Matches_MatchfromProfileId",
                table: "Profiles");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Profiles_MatchfromProfileId",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "MatchfromProfileId",
                table: "Profiles");
        }
    }
}
