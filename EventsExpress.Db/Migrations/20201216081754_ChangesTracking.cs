using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ChangesTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EventSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "EventSchedules");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "EventSchedules");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "EventSchedules");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Events");

            migrationBuilder.CreateTable(
                name: "ChangeInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityKeys = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    PropertyChangesText = table.Column<string>(nullable: true),
                    ChangesType = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "EventSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "EventSchedules",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "EventSchedules",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "EventSchedules",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiedBy",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Events",
                type: "datetime2",
                nullable: true);
        }
    }
}
