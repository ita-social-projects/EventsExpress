using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddOccurenceEventTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Events",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Events",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Events",
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Events",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.CreateTable(
                name: "OccurenceEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedBy = table.Column<Guid>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Frequency = table.Column<int>(nullable: false),
                    LastRun = table.Column<DateTime>(nullable: false),
                    NextRun = table.Column<DateTime>(nullable: false),
                    Periodicity = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    EventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OccurenceEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OccurenceEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OccurenceEvents_EventId",
                table: "OccurenceEvents",
                column: "EventId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OccurenceEvents");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Events");
        }
    }
}
