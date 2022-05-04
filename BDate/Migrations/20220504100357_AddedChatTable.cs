using Microsoft.EntityFrameworkCore.Migrations;

namespace BDate.Migrations
{
    public partial class AddedChatTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    fromProfileId = table.Column<string>(type: "text", nullable: false),
                    toProfileId = table.Column<string>(type: "text", nullable: false),
                    roomId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => new { x.fromProfileId, x.toProfileId });
                    table.ForeignKey(
                        name: "FK_Chats_Profiles_fromProfileId",
                        column: x => x.fromProfileId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chats");
        }
    }
}
