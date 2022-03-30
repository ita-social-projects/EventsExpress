using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventsExpress.Db.Migrations
{
    public partial class AddRoleNotificationTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "Message", "Subject", "Title" },
                values: new object[] { 7, "Dear {{UserEmail}}, your roles were changed. Your current roles are: {{FormattedRoles}}.", "Your roles were changed", "RolesChanged" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
