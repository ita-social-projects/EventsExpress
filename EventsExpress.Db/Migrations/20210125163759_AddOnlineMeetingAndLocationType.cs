using Microsoft.EntityFrameworkCore.Migrations;

namespace EventsExpress.Db.Migrations
{
    public partial class AddOnlineMeetingAndLocationType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OnlineMeeting",
                table: "EventLocations",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EventLocations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnlineMeeting",
                table: "EventLocations");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EventLocations");
        }
    }
}
