﻿using System;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.Helpers;

namespace EventsExpress.Db.DbInitialize
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext dbContext)
        {
            dbContext.Database.EnsureCreated();

            // Look for any users
            if (dbContext.Users.Any())
            {
                return; // DB has been seeded
            }

            Role adminRole = new Role { Name = "Admin" };
            Role userRole = new Role { Name = "User" };
            dbContext.Roles.AddRange(new Role[] { adminRole, userRole });

            var saltDef = PasswordHasher.GenerateSalt();
            var users = new User[]
            {
                 new User
                 {
                     Name = "Admin",
                     PasswordHash = PasswordHasher.GenerateHash("1qaz1qaz", saltDef),
                     Salt = saltDef,
                     Email = "admin@gmail.com",
                     EmailConfirmed = true,
                     Phone = "+380974293583",
                     Birthday = DateTime.Parse("2000-01-01"),
                     Gender = Gender.Male,
                     IsBlocked = false,
                     Role = adminRole,
                 },

                 new User
                  {
                      Name = "UserTest",
                      PasswordHash = PasswordHasher.GenerateHash("1qaz1qaz", saltDef),
                      Salt = saltDef,
                      Email = "user@gmail.com",
                      EmailConfirmed = true,
                      Phone = "+380970101013",
                      Birthday = DateTime.Parse("2000-01-01"),
                      Gender = Gender.Male,
                      IsBlocked = false,
                      Role = userRole,
                  },
            };

            dbContext.Users.AddRange(users);

            var categories = new Category[]
            {
                new Category { Name = "Sea" },
                new Category { Name = "Mount" },
                new Category { Name = "Summer" },
                new Category { Name = "Golf" },
                new Category { Name = "Team-Building" },
                new Category { Name = "Swimming" },
                new Category { Name = "Gaming" },
                new Category { Name = "Fishing" },
                new Category { Name = "Trips" },
                new Category { Name = "Meeting" },
                new Category { Name = "Sport" },
            };

            dbContext.Categories.AddRange(categories);

            var emailMessages = new NotificationTemplate[]
            {
                new NotificationTemplate { Title = "BlockedUser", Subject = "Your account was blocked", MessageText = "Dear (UserName), your account was blocked for some reason!" },
                new NotificationTemplate { Title = "CreateEventVerification", Subject = "Approve your recurrent event!", MessageText = "Follow the <a href='(link)'>link</a> to create the recurrent event." },
                new NotificationTemplate { Title = "EventCreated", Subject = "New event for you!", MessageText = "The <a href='(link)'>event</a> was created which could interested you." },
                new NotificationTemplate { Title = "EventStatusCanceled", Subject = "The event you have been joined was canceled",  MessageText = "Dear (UserName), the event you have been joined was CANCELED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Title = "EventStatusBlocked", Subject = "The event you have been joined was blocked",  MessageText = "Dear (UserName), the event you have been joined was BLOCKED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Title = "EventStatusActivated", Subject = "The event you have been joined was activated",  MessageText = "Dear (UserName), the event you have been joined was ACTIVATED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Title = "ParticipationApproved", Subject = "Approving participation", MessageText = "Dear (UserName), you have been approved to join to this event. To check it, please, visit \"<a href='(link)'>EventExpress</a>\"" },
                new NotificationTemplate { Title = "ParticipationDenied", Subject = "Denying participation", MessageText = "Dear (UserName), you have been denied to join to this event. To check it, please, visit \"<a href='(link)'>EventExpress</a>\"" },
                new NotificationTemplate { Title = "RegisterVerification", Subject = "EventExpress registration", MessageText = "For confirm your email please follow the <a href='(link)'>link</a>" },
                new NotificationTemplate { Title = "UnblockedUser", Subject = "Your account was Unblocked", MessageText = "Dear (UserName), congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress" },
            };

            dbContext.NotificationTemplates.AddRange(emailMessages);

            dbContext.SaveChanges();
        }
    }
}
