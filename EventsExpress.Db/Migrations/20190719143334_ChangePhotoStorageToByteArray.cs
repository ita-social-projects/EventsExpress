using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ChangePhotoStorageToByteArray : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Path",
                table: "Photos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Thumb",
                table: "Photos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "Thumb",
                table: "Photos");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "Photos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Photos",
                nullable: true);
        }
    }
}
