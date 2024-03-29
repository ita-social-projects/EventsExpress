﻿using System;
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
               new NotificationTemplate { Id = NotificationProfile.RegisterVerification, Title = "RegisterVerification", Subject = "EventExpress registration", Message = "For confirm your email please follow the <a href='{{EmailLink}}'>link</a>" },
               new NotificationTemplate { Id = NotificationProfile.UnblockedUser, Title = "UnblockedUser", Subject = "Your account was Unblocked", Message = "Dear {{UserEmail}}, congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress" },
               new NotificationTemplate { Id = NotificationProfile.OwnEventChanged, Title = "OwnEventChanged", Subject = "Your event was changed", Message = "Dear {{UserEmail}}, your <a href='{{EventLink}}'>event</a> has been changed." },
               new NotificationTemplate { Id = NotificationProfile.JoinedEventChanged, Title = "JoinedEventChanged", Subject = "Joined event was changed", Message = "Dear {{UserEmail}}, your joined <a href='{{EventLink}}'>event</a> has been changed." },
               new NotificationTemplate { Id = NotificationProfile.RoleChanged, Title = "RolesChanged", Subject = "Your roles were changed", Message = "Dear {{UserEmail}}, your roles were changed. Your current roles are: {{FormattedRoles}}." },
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
