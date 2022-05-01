using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class AddedMatchTableStructureUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "toProfileId",
                table: "Matches");

            migrationBuilder.RenameColumn(
                name: "fromProfileId",
                table: "Matches",
                newName: "matchedProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "matchedProfileId",
                table: "Matches",
                newName: "fromProfileId");

            migrationBuilder.AddColumn<string>(
                name: "toProfileId",
                table: "Matches",
                type: "text",
                nullable: true);
        }
    }
}
