using Microsoft.EntityFrameworkCore.Migrations;

namespace Ticketing.Server.Data.Migrations
{
    public partial class nullableParentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ParentTicketId",
                table: "Tickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets",
                column: "ParentTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "ParentTicketId",
                table: "Tickets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets",
                column: "ParentTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
