using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class AddedInventoryMovements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "inventories",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "inventory_code",
                table: "inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_locked",
                table: "inventories",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "sku",
                table: "inventories",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "inventory_movements",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    inventory_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    receiver_id = table.Column<Guid>(type: "uuid", nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_movements", x => x.id);
                    table.ForeignKey(
                        name: "fk_inventory_movements_inventories_inventory_id",
                        column: x => x.inventory_id,
                        principalTable: "inventories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inventory_movements_users_receiver_id",
                        column: x => x.receiver_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_inventory_movements_users_sender_id",
                        column: x => x.sender_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventories_sku",
                table: "inventories",
                column: "sku",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_inventory_movements_inventory_id",
                table: "inventory_movements",
                column: "inventory_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_movements_receiver_id",
                table: "inventory_movements",
                column: "receiver_id");

            migrationBuilder.CreateIndex(
                name: "ix_inventory_movements_sender_id",
                table: "inventory_movements",
                column: "sender_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "inventory_movements");

            migrationBuilder.DropIndex(
                name: "ix_inventories_sku",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "inventory_code",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "is_locked",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "sku",
                table: "inventories");
        }
    }
}
