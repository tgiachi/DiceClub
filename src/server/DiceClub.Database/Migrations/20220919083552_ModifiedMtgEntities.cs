using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class ModifiedMtgEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "mtg_dump",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "set_code",
                table: "mtg_dump",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "cards_staging",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards_staging", x => x.id);
                    table.ForeignKey(
                        name: "fk_cards_staging_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cards_staging_user_id",
                table: "cards_staging",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards_staging");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "mtg_dump");

            migrationBuilder.DropColumn(
                name: "set_code",
                table: "mtg_dump");
        }
    }
}
