using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class UpdateRateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rates_Users_UserToId",
                table: "Rates");

            migrationBuilder.DropIndex(
                name: "IX_Rates_UserToId",
                table: "Rates");

            migrationBuilder.DropColumn(
                name: "UserToId",
                table: "Rates");

            migrationBuilder.AlterColumn<byte>(
                name: "Score",
                table: "Rates",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Score",
                table: "Rates",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AddColumn<Guid>(
                name: "UserToId",
                table: "Rates",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Rates_UserToId",
                table: "Rates",
                column: "UserToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rates_Users_UserToId",
                table: "Rates",
                column: "UserToId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
