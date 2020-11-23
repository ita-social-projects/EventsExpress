using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddDefaultDatetimeValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "EventSchedules",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(8703),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "EventSchedules",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(8321),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(6334),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Events",
                nullable: false,
                defaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(5144),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "EventSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(8703));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "EventSchedules",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(8321));

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDateTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(6334));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Events",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2020, 11, 21, 11, 35, 55, 387, DateTimeKind.Utc).AddTicks(5144));
        }
    }
}
