using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class OwnersToOrganizersRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventOwners_Events_EventId",
                table: "EventOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOwners_Users_UserId",
                table: "EventOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners");

            migrationBuilder.RenameTable(
                name: "EventOwners",
                newName: "EventOrganizers");

            migrationBuilder.RenameIndex(
                name: "IX_EventOwners_UserId_EventId",
                table: "EventOrganizers",
                newName: "IX_EventOrganizers_UserId_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventOwners_EventId",
                table: "EventOrganizers",
                newName: "IX_EventOrganizers_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventOrganizers",
                table: "EventOrganizers",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganizers_Events_EventId",
                table: "EventOrganizers",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOrganizers_Users_UserId",
                table: "EventOrganizers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganizers_Events_EventId",
                table: "EventOrganizers");

            migrationBuilder.DropForeignKey(
                name: "FK_EventOrganizers_Users_UserId",
                table: "EventOrganizers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventOrganizers",
                table: "EventOrganizers");

            migrationBuilder.RenameTable(
                name: "EventOrganizers",
                newName: "EventOwners");

            migrationBuilder.RenameIndex(
                name: "IX_EventOrganizers_UserId_EventId",
                table: "EventOwners",
                newName: "IX_EventOwners_UserId_EventId");

            migrationBuilder.RenameIndex(
                name: "IX_EventOrganizers_EventId",
                table: "EventOwners",
                newName: "IX_EventOwners_EventId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.AddForeignKey(
                name: "FK_EventOwners_Events_EventId",
                table: "EventOwners",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EventOwners_Users_UserId",
                table: "EventOwners",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
