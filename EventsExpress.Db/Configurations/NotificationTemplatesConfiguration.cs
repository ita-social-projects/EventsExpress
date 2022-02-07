using System;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations
{
    public class NotificationTemplatesConfiguration : IEntityTypeConfiguration<NotificationTemplate>
    {
        public void Configure(EntityTypeBuilder<NotificationTemplate> builder)
        {
            builder.HasData(new[]
            {
               new NotificationTemplate { Id = NotificationProfile.BlockedUser, Title = "BlockedUser", Subject = "Your account was blocked", Message = "Dear {{UserEmail}}, your account was blocked for some reason!" },
               new NotificationTemplate { Id = NotificationProfile.CreateEventVerification, Title = "CreateEventVerification", Subject = "Approve your recurrent event!", Message = "Follow the <a href='{{EventScheduleLink}}'>link</a> to create the recurrent event." },
               new NotificationTemplate { Id = NotificationProfile.EventCreated, Title = "EventCreated", Subject = "New event for you!", Message = "The <a href='{{EventLink}}'>event</a> was created which could interested you." },
               new NotificationTemplate { Id = NotificationProfile.EventStatusCanceled, Title = "EventStatusCanceled", Subject = "The event you have been joined was canceled",  Message = "Dear {{UserEmail}}, the event you have been joined was CANCELED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"" },
               new NotificationTemplate { Id = NotificationProfile.EventStatusBlocked, Title = "EventStatusBlocked", Subject = "The event you have been joined was blocked",  Message = "Dear {{UserEmail}}, the event you have been joined was BLOCKED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"" },
               new NotificationTemplate { Id = NotificationProfile.EventStatusActivated, Title = "EventStatusActivated", Subject = "The event you have been joined was activated",  Message = "Dear {{UserEmail}}, the event you have been joined was ACTIVATED. The reason is: {{Reason}} \"<a href='{{EventLink}}'>{{Title}}</a>\"" },
               new NotificationTemplate { Id = NotificationProfile.ParticipationApproved, Title = "ParticipationApproved", Subject = "Approving participation", Message = "Dear {{UserEmail}}, you have been approved to join to this event. To check it, please, visit \"<a href='{{EventLink}}'>EventExpress</a>\"" },
               new NotificationTemplate { Id = NotificationProfile.ParticipationDenied, Title = "ParticipationDenied", Subject = "Denying participation", Message = "Dear {{UserEmail}}, you have been denied to join to this event. To check it, please, visit \"<a href='{{EventLink}}'>EventExpress</a>\"" },
               new NotificationTemplate { Id = NotificationProfile.RegisterVerification, Title = "RegisterVerification", Subject = "EventExpress registration", Message = "For confirm your email please follow the <a href='{{EmailLink}}'>link</a>" },
               new NotificationTemplate { Id = NotificationProfile.UnblockedUser, Title = "UnblockedUser", Subject = "Your account was Unblocked", Message = "Dear {{UserEmail}}, congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress" },
               new NotificationTemplate { Id = NotificationProfile.OwnEventChanged, Title = "OwnEventChanged", Subject = "Your event was changed", Message = "Dear {{UserEmail}}, your event has been changed." },
               new NotificationTemplate { Id = NotificationProfile.JoinedEventChanged, Title = "JoinedEventChanged", Subject = "Joined event was changed", Message = "Dear {{UserEmail}}, your joined event has been changed." },
            });
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedNever();
            builder.Property(c => c.Title)
                .IsRequired();
            builder.Property(c => c.Subject)
                .IsRequired();
            builder.Property(c => c.Message)
                .IsRequired();
        }
    }
}
