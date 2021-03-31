using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ImplementDraft : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "MaxParticipants",
                table: "Events",
                nullable: true,
                defaultValue: 2147483647,
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValue: 2147483647);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "Events",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "EventLocationId",
                table: "Events",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "Events",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "Events",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events",
                column: "EventLocationId",
                principalTable: "EventLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "MaxParticipants",
                table: "Events",
                type: "int",
                nullable: false,
                defaultValue: 2147483647,
                oldClrType: typeof(int),
                oldNullable: true,
                oldDefaultValue: 2147483647);

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublic",
                table: "Events",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EventLocationId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateTo",
                table: "Events",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateFrom",
                table: "Events",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventLocations_EventLocationId",
                table: "Events",
                column: "EventLocationId",
                principalTable: "EventLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
