using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BDate.Migrations
{
    public partial class UpdatedOverallDbStructure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hobbies",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "Personal",
                table: "Profiles");

            migrationBuilder.DropColumn(
                name: "isActive",
                table: "Profiles");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyId = table.Column<string>(type: "text", nullable: false),
                    HobbyName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyId);
                });

            migrationBuilder.CreateTable(
                name: "Personalities",
                columns: table => new
                {
                    PersonalityId = table.Column<string>(type: "text", nullable: false),
                    PersonalityName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personalities", x => x.PersonalityId);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    SettingId = table.Column<string>(type: "text", nullable: false),
                    isHiddenAge = table.Column<bool>(type: "boolean", nullable: false),
                    isHiddenLastName = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.SettingId);
                    table.ForeignKey(
                        name: "FK_Settings_Profiles_SettingId",
                        column: x => x.SettingId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserToCategories",
                columns: table => new
                {
                    Relation_ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    User_ID = table.Column<long>(type: "bigint", nullable: false),
                    Kategorijas_ID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToCategories", x => x.Relation_ID);
                });

            migrationBuilder.CreateTable(
                name: "HobbyProfile",
                columns: table => new
                {
                    HobbiesHobbyId = table.Column<string>(type: "text", nullable: false),
                    ProfilesUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HobbyProfile", x => new { x.HobbiesHobbyId, x.ProfilesUserId });
                    table.ForeignKey(
                        name: "FK_HobbyProfile_Hobbies_HobbiesHobbyId",
                        column: x => x.HobbiesHobbyId,
                        principalTable: "Hobbies",
                        principalColumn: "HobbyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HobbyProfile_Profiles_ProfilesUserId",
                        column: x => x.ProfilesUserId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityProfile",
                columns: table => new
                {
                    PersonalitiesPersonalityId = table.Column<string>(type: "text", nullable: false),
                    ProfilesUserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityProfile", x => new { x.PersonalitiesPersonalityId, x.ProfilesUserId });
                    table.ForeignKey(
                        name: "FK_PersonalityProfile_Personalities_PersonalitiesPersonalityId",
                        column: x => x.PersonalitiesPersonalityId,
                        principalTable: "Personalities",
                        principalColumn: "PersonalityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalityProfile_Profiles_ProfilesUserId",
                        column: x => x.ProfilesUserId,
                        principalTable: "Profiles",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HobbyProfile_ProfilesUserId",
                table: "HobbyProfile",
                column: "ProfilesUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityProfile_ProfilesUserId",
                table: "PersonalityProfile",
                column: "ProfilesUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HobbyProfile");

            migrationBuilder.DropTable(
                name: "PersonalityProfile");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DropTable(
                name: "UserToCategories");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Personalities");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Profiles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Hobbies",
                table: "Profiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Personal",
                table: "Profiles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "isActive",
                table: "Profiles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
