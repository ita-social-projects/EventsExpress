using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddUserMoreInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserMoreInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    ParentStatus = table.Column<int>(nullable: false),
                    RelationShipStatus = table.Column<int>(nullable: false),
                    TheTypeOfLeisure = table.Column<int>(nullable: false),
                    AditionalInfoAboutUser = table.Column<string>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMoreInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMoreInfo_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMoreInfoEventType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserMoreInfoId = table.Column<Guid>(nullable: false),
                    EventType = table.Column<int>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    UserMoreInfoId = table.Column<Guid>(nullable: false),
                    ReasonsForUsingTheSite = table.Column<int>(nullable: false),
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
                name: "IX_UserMoreInfo_UserId",
                table: "UserMoreInfo",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMoreInfoEventType_UserMoreInfoId",
                table: "UserMoreInfoEventType",
                column: "UserMoreInfoId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_UserMoreInfoReasonsForUsingTheSite_UserMoreInfoId",
                table: "UserMoreInfoReasonsForUsingTheSite",
                column: "UserMoreInfoId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMoreInfoEventType");

            migrationBuilder.DropTable(
                name: "UserMoreInfoReasonsForUsingTheSite");

            migrationBuilder.DropTable(
                name: "UserMoreInfo");
        }
    }
}
