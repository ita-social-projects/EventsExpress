using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class AddedEventAudience : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EventAudienceId",
                table: "Events",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "EventLocations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "EventAudiences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOnlyForAdults = table.Column<bool>(type: "bit", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventAudiences", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_EventAudienceId",
                table: "Events",
                column: "EventAudienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_EventAudiences_EventAudienceId",
                table: "Events",
                column: "EventAudienceId",
                principalTable: "EventAudiences",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_EventAudiences_EventAudienceId",
                table: "Events");

            migrationBuilder.DropTable(
                name: "EventAudiences");

            migrationBuilder.DropIndex(
                name: "IX_Events_EventAudienceId",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "EventAudienceId",
                table: "Events");

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "EventLocations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
