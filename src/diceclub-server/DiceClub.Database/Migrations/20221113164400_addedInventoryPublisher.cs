using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedInventoryPublisher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "publisher_id",
                table: "inventories",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "inventory_publishers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    createddate = table.Column<DateTime>(name: "created_date", type: "timestamp without time zone", nullable: false),
                    updateddate = table.Column<DateTime>(name: "updated_date", type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_inventory_publishers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_inventories_publisher_id",
                table: "inventories",
                column: "publisher_id");

            migrationBuilder.AddForeignKey(
                name: "fk_inventories_inventory_publishers_publisher_id",
                table: "inventories",
                column: "publisher_id",
                principalTable: "inventory_publishers",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_inventories_inventory_publishers_publisher_id",
                table: "inventories");

            migrationBuilder.DropTable(
                name: "inventory_publishers");

            migrationBuilder.DropIndex(
                name: "ix_inventories_publisher_id",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "publisher_id",
                table: "inventories");
        }
    }
}
