using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class Inventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnitOfMeasurings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UnitName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasurings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NeedQuantity = table.Column<double>(nullable: false),
                    ItemName = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false),
                    UnitOfMeasuringId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventories_UnitOfMeasurings_UnitOfMeasuringId",
                        column: x => x.UnitOfMeasuringId,
                        principalTable: "UnitOfMeasurings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEventInventories",
                columns: table => new
                {
                    EventId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    InventoryId = table.Column<Guid>(nullable: false),
                    Quantity = table.Column<double>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEventInventories", x => new { x.InventoryId, x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEventInventories_Inventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserEventInventories_UserEvent_UserId_EventId",
                        columns: x => new { x.UserId, x.EventId },
                        principalTable: "UserEvent",
                        principalColumns: new[] { "UserId", "EventId" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_EventId",
                table: "Inventories",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_UnitOfMeasuringId",
                table: "Inventories",
                column: "UnitOfMeasuringId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEventInventories_UserId_EventId",
                table: "UserEventInventories",
                columns: new[] { "UserId", "EventId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEventInventories");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "UnitOfMeasurings");
        }
    }
}
