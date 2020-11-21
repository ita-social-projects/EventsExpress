using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class EditPropsManageableEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "OccurenceEvents");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "OccurenceEvents");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "OccurenceEvents",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "OccurenceEvents",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Events",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Events",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "OccurenceEvents");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "OccurenceEvents");

            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "ModifiedDateTime",
                table: "Events");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "OccurenceEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "OccurenceEvents",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Events",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }
    }
}
