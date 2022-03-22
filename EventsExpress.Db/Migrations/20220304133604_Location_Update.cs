using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class Location_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventLocations");

            migrationBuilder.RenameColumn(
                name: "EventLocationId",
                table: "Events",
                newName: "LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_EventLocationId",
                table: "Events",
                newName: "IX_Events_LocationId");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Point = table.Column<Point>(type: "geography", nullable: true),
                    OnlineMeeting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Locations_LocationId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Locations_LocationId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Users_LocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LocationId",
                table: "Events",
                newName: "EventLocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                newName: "IX_Events_EventLocationId");

            migrationBuilder.CreateTable(
                name: "EventLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OnlineMeeting = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Point = table.Column<Point>(type: "geography", nullable: true), Type = table.Column<int>(type: "int", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLocations", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events",
                column: "EventLocationId",
                principalTable: "EventLocations",
                principalColumn: "Id");
        }
    }
}
