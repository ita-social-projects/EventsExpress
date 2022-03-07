using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class UserMoreInfoUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMoreInfoEventType");

            migrationBuilder.DropTable(
                name: "UserMoreInfoReasonsForUsingTheSite");

            migrationBuilder.RenameColumn(
                name: "AditionalInfoAboutUser",
                table: "UserMoreInfo",
                newName: "AdditionalInfo");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TheTypeOfLeisure",
                table: "UserMoreInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RelationShipStatus",
                table: "UserMoreInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ParentStatus",
                table: "UserMoreInfo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<byte>(
                name: "EventTypes",
                table: "UserMoreInfo",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "ReasonsForUsingTheSite",
                table: "UserMoreInfo",
                type: "tinyint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "EventTypes",
                table: "UserMoreInfo");

            migrationBuilder.DropColumn(
                name: "ReasonsForUsingTheSite",
                table: "UserMoreInfo");

            migrationBuilder.RenameColumn(
                name: "AdditionalInfo",
                table: "UserMoreInfo",
                newName: "AditionalInfoAboutUser");

            migrationBuilder.AlterColumn<int>(
                name: "TheTypeOfLeisure",
                table: "UserMoreInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RelationShipStatus",
                table: "UserMoreInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ParentStatus",
                table: "UserMoreInfo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserMoreInfoEventType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMoreInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EventType = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMoreInfoEventType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMoreInfoEventType_UserMoreInfo_UserMoreInfoId",
                        column: x => x.UserMoreInfoId,
                        principalTable: "UserMoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMoreInfoReasonsForUsingTheSite",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserMoreInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReasonsForUsingTheSite = table.Column<int>(type: "int", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMoreInfoReasonsForUsingTheSite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMoreInfoReasonsForUsingTheSite_UserMoreInfo_UserMoreInfoId",
                        column: x => x.UserMoreInfoId,
                        principalTable: "UserMoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMoreInfoEventType_UserMoreInfoId",
                table: "UserMoreInfoEventType",
                column: "UserMoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMoreInfoReasonsForUsingTheSite_UserMoreInfoId",
                table: "UserMoreInfoReasonsForUsingTheSite",
                column: "UserMoreInfoId");
        }
    }
}
