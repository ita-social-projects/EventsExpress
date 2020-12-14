using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class ChangeTracking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChangeInfos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    EntityName = table.Column<string>(nullable: true),
                    EntityKeys = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    PropertyChangesText = table.Column<string>(nullable: true),
                    ChangesType = table.Column<int>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChangeInfos", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChangeInfos");
        }
    }
}
