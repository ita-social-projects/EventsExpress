using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable SA1118
#pragma warning disable SA1413

namespace EventsExpress.Db.Migrations
{
    public partial class NewNotificationTemplates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "Message", "Subject", "Title" },
                values: new object[,]
                {
                    { 1, "Dear {{UserEmail}}, your account was blocked for some reason!", "Your account was blocked", "BlockedUser" },
                    { 2, "Follow the <a href='{{EventScheduleLink}}'>link</a> to create the recurrent event.", "Approve your recurrent event!", "CreateEventVerification" },
                    { 3, "The <a href='{{EventLink}}'>event</a> was created which could interested you.", "New event for you!", "EventCreated" },
                    { 4, "Dear {{UserEmail}}, the event you have been joined was CANCELED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"", "The event you have been joined was canceled", "EventStatusCanceled" },
                    { 5, "Dear {{UserEmail}}, the event you have been joined was BLOCKED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"", "The event you have been joined was blocked", "EventStatusBlocked" },
                    { 6, "Dear {{UserEmail}}, the event you have been joined was ACTIVATED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"", "The event you have been joined was activated", "EventStatusActivated" },
                    { 7, "Dear {{UserEmail}}, you have been approved to join to this event. To check it, please, visit \"<a href='{{EventLink}}'>EventExpress</a>\"", "Approving participation", "ParticipationApproved" },
                    { 8, "Dear {{UserEmail}}, you have been denied to join to this event. To check it, please, visit \"<a href='{{EventLink}}'>EventExpress</a>\"", "Denying participation", "ParticipationDenied" },
                    { 9, "For confirm your email please follow the <a href='{{EmailLink}}'>link</a>", "EventExpress registration", "RegisterVerification" },
                    { 10, "Dear {{UserEmail}}, congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress", "Your account was Unblocked", "UnblockedUser" },
                    { 11, "Dear {{UserEmail}}, your event has been changed.", "Your event was changed", "OwnEventChanged" },
                    { 12, "Dear {{UserEmail}}, your joined event has been changed.", "Joined event was changed", "JoinedEventChanged" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 12);
        }
    }
}
