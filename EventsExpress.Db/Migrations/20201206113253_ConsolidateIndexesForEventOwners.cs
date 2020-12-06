using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ConsilidateIndexesForEventOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners");

            migrationBuilder.DropIndex(
                name: "IX_EventOwners_UserId",
                table: "EventOwners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners",
                columns: new[] { "UserId", "EventId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventOwners_EventId",
                table: "EventOwners",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventOwners_UserId_EventId",
                table: "EventOwners",
                columns: new[] { "UserId", "EventId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners");

            migrationBuilder.DropIndex(
                name: "IX_EventOwners_EventId",
                table: "EventOwners");

            migrationBuilder.DropIndex(
                name: "IX_EventOwners_UserId_EventId",
                table: "EventOwners");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventOwners",
                table: "EventOwners",
                columns: new[] { "EventId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_EventOwners_UserId",
                table: "EventOwners",
                column: "UserId");
        }
    }
}
