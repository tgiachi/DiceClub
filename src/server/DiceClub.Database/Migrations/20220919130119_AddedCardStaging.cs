using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class AddedCardStaging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_added",
                table: "cards_staging",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_foil",
                table: "cards_staging",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "language",
                table: "cards_staging",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "mtg_id",
                table: "cards_staging",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_added",
                table: "cards_staging");

            migrationBuilder.DropColumn(
                name: "is_foil",
                table: "cards_staging");

            migrationBuilder.DropColumn(
                name: "language",
                table: "cards_staging");

            migrationBuilder.DropColumn(
                name: "mtg_id",
                table: "cards_staging");
        }
    }
}
