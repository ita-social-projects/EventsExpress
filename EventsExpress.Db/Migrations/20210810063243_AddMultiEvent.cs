using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddMultiEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMultiEvent",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MultiEventStatus",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParentId = table.Column<Guid>(nullable: false),
                    ChildId = table.Column<Guid>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiEventStatus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MultiEventStatus_Events_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiEventStatus_Events_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiEventStatus_ChildId",
                table: "MultiEventStatus",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiEventStatus_ParentId",
                table: "MultiEventStatus",
                column: "ParentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultiEventStatus");

            migrationBuilder.DropColumn(
                name: "IsMultiEvent",
                table: "Events");
        }
    }
}
