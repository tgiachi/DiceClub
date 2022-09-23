using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class UpdatedCardStageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "mtg_id",
                table: "cards_staging");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mtg_languages",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "mtg_languages",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "quantity",
                table: "cards_staging",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "scryfall_id",
                table: "cards_staging",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "quantity",
                table: "cards_staging");

            migrationBuilder.DropColumn(
                name: "scryfall_id",
                table: "cards_staging");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "mtg_languages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "mtg_languages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(5)",
                oldMaxLength: 5);

            migrationBuilder.AddColumn<int>(
                name: "mtg_id",
                table: "cards_staging",
                type: "integer",
                nullable: true);
        }
    }
}
