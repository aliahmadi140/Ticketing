using Microsoft.EntityFrameworkCore.Migrations;

namespace Ticketing.Server.Data.Migrations
{
    public partial class addTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Text",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Text",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Tickets");
        }
    }
}
