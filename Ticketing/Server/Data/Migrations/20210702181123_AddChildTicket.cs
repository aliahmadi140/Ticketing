using Microsoft.EntityFrameworkCore.Migrations;

namespace Ticketing.Server.Data.Migrations
{
    public partial class AddChildTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ParentTicketId",
                table: "Tickets",
                newName: "ChildTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ParentTicketId",
                table: "Tickets",
                newName: "IX_Tickets_ChildTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_ChildTicketId",
                table: "Tickets",
                column: "ChildTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Tickets_ChildTicketId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "ChildTicketId",
                table: "Tickets",
                newName: "ParentTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_ChildTicketId",
                table: "Tickets",
                newName: "IX_Tickets_ParentTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Tickets_ParentTicketId",
                table: "Tickets",
                column: "ParentTicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
