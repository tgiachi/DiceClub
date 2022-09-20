using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class AddedDeckEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "deck_master",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deck_master", x => x.id);
                    table.ForeignKey(
                        name: "fk_deck_master_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "deck_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    deck_master_id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    card_type = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_deck_details", x => x.id);
                    table.ForeignKey(
                        name: "fk_deck_details_deck_master_deck_master_id",
                        column: x => x.deck_master_id,
                        principalTable: "deck_master",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_deck_details_mtg_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "mtg_cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_deck_details_card_id",
                table: "deck_details",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_deck_details_deck_master_id",
                table: "deck_details",
                column: "deck_master_id");

            migrationBuilder.CreateIndex(
                name: "ix_deck_master_name_owner_id",
                table: "deck_master",
                columns: new[] { "name", "owner_id" });

            migrationBuilder.CreateIndex(
                name: "ix_deck_master_owner_id",
                table: "deck_master",
                column: "owner_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "deck_details");

            migrationBuilder.DropTable(
                name: "deck_master");
        }
    }
}
