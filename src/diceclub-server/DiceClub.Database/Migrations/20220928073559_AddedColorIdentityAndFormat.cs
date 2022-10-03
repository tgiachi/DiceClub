using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class AddedColorIdentityAndFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "card_count",
                table: "deck_master",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "color_identity",
                table: "deck_master",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "format",
                table: "deck_master",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "card_count",
                table: "deck_master");

            migrationBuilder.DropColumn(
                name: "color_identity",
                table: "deck_master");

            migrationBuilder.DropColumn(
                name: "format",
                table: "deck_master");
        }
    }
}
