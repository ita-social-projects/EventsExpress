using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddContactAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactAdmin",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SenderId = table.Column<Guid>(nullable: true),
                    AssigneeId = table.Column<Guid>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Subject = table.Column<byte>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    EmailBody = table.Column<string>(nullable: true),
                    ResolutionDetails = table.Column<string>(nullable: true),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactAdmin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactAdmin_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactAdmin_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChangeInfos_UserId",
                table: "ChangeInfos",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAdmin_AssigneeId",
                table: "ContactAdmin",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactAdmin_SenderId",
                table: "ContactAdmin",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChangeInfos_Users_UserId",
                table: "ChangeInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChangeInfos_Users_UserId",
                table: "ChangeInfos");

            migrationBuilder.DropTable(
                name: "ContactAdmin");

            migrationBuilder.DropIndex(
                name: "IX_ChangeInfos_UserId",
                table: "ChangeInfos");
        }
    }
}
