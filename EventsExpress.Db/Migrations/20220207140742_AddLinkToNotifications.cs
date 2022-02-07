using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class AddLinkToNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 11,
                column: "Message",
                value: "Dear {{UserEmail}}, your <a href='{{EventLink}}'>event</a> has been changed.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 12,
                column: "Message",
                value: "Dear {{UserEmail}}, your joined <a href='{{EventLink}}'>event</a> has been changed.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 11,
                column: "Message",
                value: "Dear {{UserEmail}}, your event has been changed.");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 12,
                column: "Message",
                value: "Dear {{UserEmail}}, your joined event has been changed.");
        }
    }
}
