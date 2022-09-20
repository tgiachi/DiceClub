using System;
using DiceClub.Api.Data.Mtg;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    group_name = table.Column<string>(type: "text", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_groups", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_card_legality_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_card_legality_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_card_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_card_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_cards_dump",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    multiverse_id = table.Column<int>(type: "integer", nullable: true),
                    card = table.Column<MtgCard>(type: "jsonb", nullable: false),
                    card_name = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    foreign_names = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    set_code = table.Column<string>(type: "text", nullable: false),
                    search_vector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false)
                        .Annotation("Npgsql:TsVectorConfig", "italian")
                        .Annotation("Npgsql:TsVectorProperties", new[] { "card_name", "foreign_names" }),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_cards_dump", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_colors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    description = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    image_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_colors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_languages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    code = table.Column<string>(type: "text", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_languages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_legalities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_legalities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_rarities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    image = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_rarities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "mtg_sets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    description = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    image = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_sets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    nick_name = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false),
                    serial_id = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    refresh_token = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cards_staging",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mtg_id = table.Column<int>(type: "integer", nullable: true),
                    is_foil = table.Column<bool>(type: "boolean", nullable: false),
                    language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_added = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cards_staging", x => x.id);
                    table.ForeignKey(
                        name: "fk_cards_staging_mtg_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "mtg_languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cards_staging_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtg_cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    scryfall_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    mtg_id = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    printed_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    type_line = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    language_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mana_cost = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cmc = table.Column<double>(type: "double precision", nullable: true),
                    power = table.Column<int>(type: "integer", nullable: false),
                    toughness = table.Column<int>(type: "integer", nullable: false),
                    collector_number = table.Column<int>(type: "integer", nullable: false),
                    set_id = table.Column<Guid>(type: "uuid", nullable: false),
                    rarity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    low_res_image_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    high_res_image_url = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: false),
                    search_vector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false)
                        .Annotation("Npgsql:TsVectorConfig", "italian")
                        .Annotation("Npgsql:TsVectorProperties", new[] { "name", "description", "type_line", "printed_name" }),
                    is_color_less = table.Column<bool>(type: "boolean", nullable: false),
                    is_multi_color = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtg_cards_mtg_card_types_type_id",
                        column: x => x.type_id,
                        principalTable: "mtg_card_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_mtg_languages_language_id",
                        column: x => x.language_id,
                        principalTable: "mtg_languages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_mtg_rarities_rarity_id",
                        column: x => x.rarity_id,
                        principalTable: "mtg_rarities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_mtg_sets_set_id",
                        column: x => x.set_id,
                        principalTable: "mtg_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_groups",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    group_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_groups", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_groups_groups_group_id",
                        column: x => x.group_id,
                        principalTable: "groups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_groups_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtg_cards_legalities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_legality_id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_legality_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_cards_legalities", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtg_cards_legalities_mtg_card_legality_types_card_legality_",
                        column: x => x.card_legality_type_id,
                        principalTable: "mtg_card_legality_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_legalities_mtg_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "mtg_cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_cards_legalities_mtg_legalities_card_legality_id",
                        column: x => x.card_legality_id,
                        principalTable: "mtg_legalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "mtg_colors_cards",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    card_id = table.Column<Guid>(type: "uuid", nullable: false),
                    color_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mtg_colors_cards", x => x.id);
                    table.ForeignKey(
                        name: "fk_mtg_colors_cards_mtg_cards_card_id",
                        column: x => x.card_id,
                        principalTable: "mtg_cards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mtg_colors_cards_mtg_colors_color_id",
                        column: x => x.color_id,
                        principalTable: "mtg_colors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cards_staging_language_id",
                table: "cards_staging",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_cards_staging_user_id",
                table: "cards_staging",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_language_id",
                table: "mtg_cards",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_name_printed_name",
                table: "mtg_cards",
                columns: new[] { "name", "printed_name" });

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_owner_id",
                table: "mtg_cards",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_rarity_id",
                table: "mtg_cards",
                column: "rarity_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_scryfall_id",
                table: "mtg_cards",
                column: "scryfall_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_search_vector",
                table: "mtg_cards",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_set_id",
                table: "mtg_cards",
                column: "set_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_type_id_rarity_id_set_id",
                table: "mtg_cards",
                columns: new[] { "type_id", "rarity_id", "set_id" });

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_dump_search_vector",
                table: "mtg_cards_dump",
                column: "search_vector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_legalities_card_id",
                table: "mtg_cards_legalities",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_legalities_card_legality_id",
                table: "mtg_cards_legalities",
                column: "card_legality_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_cards_legalities_card_legality_type_id",
                table: "mtg_cards_legalities",
                column: "card_legality_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_colors_cards_card_id",
                table: "mtg_colors_cards",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "ix_mtg_colors_cards_color_id",
                table: "mtg_colors_cards",
                column: "color_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_group_id",
                table: "user_groups",
                column: "group_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_groups_user_id",
                table: "user_groups",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cards_staging");

            migrationBuilder.DropTable(
                name: "mtg_cards_dump");

            migrationBuilder.DropTable(
                name: "mtg_cards_legalities");

            migrationBuilder.DropTable(
                name: "mtg_colors_cards");

            migrationBuilder.DropTable(
                name: "user_groups");

            migrationBuilder.DropTable(
                name: "mtg_card_legality_types");

            migrationBuilder.DropTable(
                name: "mtg_legalities");

            migrationBuilder.DropTable(
                name: "mtg_cards");

            migrationBuilder.DropTable(
                name: "mtg_colors");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropTable(
                name: "mtg_card_types");

            migrationBuilder.DropTable(
                name: "mtg_languages");

            migrationBuilder.DropTable(
                name: "mtg_rarities");

            migrationBuilder.DropTable(
                name: "mtg_sets");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
