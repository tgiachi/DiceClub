using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    /// <inheritdoc />
    public partial class addedInventoryProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ean13",
                table: "inventories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "inventory_status",
                table: "inventories",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "sold_date_time",
                table: "inventories",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sold_market",
                table: "inventories",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "sold_price",
                table: "inventories",
                type: "numeric",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ean13",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "inventory_status",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "sold_date_time",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "sold_market",
                table: "inventories");

            migrationBuilder.DropColumn(
                name: "sold_price",
                table: "inventories");
        }
    }
}
