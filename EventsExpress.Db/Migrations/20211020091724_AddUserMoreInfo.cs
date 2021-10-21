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
                    EventType = table.Column<int>(nullable: false),
                    RelationShipStatus = table.Column<int>(nullable: false),
                    TheTypeOfLeisure = table.Column<int>(nullable: false),
                    ReasonsForUsingTheSite = table.Column<int>(nullable: false),
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

            migrationBuilder.CreateIndex(
                name: "IX_UserMoreInfo_UserId",
                table: "UserMoreInfo",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserMoreInfo");
        }
    }
}
