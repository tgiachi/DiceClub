using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiceClub.Database.Migrations
{
    public partial class AddedRefreshTokenInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "refresh_token",
                table: "users",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refresh_token",
                table: "users");
        }
    }
}
