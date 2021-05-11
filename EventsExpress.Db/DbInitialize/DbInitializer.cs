using System;
using System.Linq;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

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

            var saltDef = PasswordHasher.GenerateSalt();
            var users = new User[]
            {
                new User
                {
                    Name = "Admin",
                    Email = "admin@gmail.com",
                    Phone = "+380974293583",
                    Birthday = DateTime.Parse("2000-01-01"),
                    Gender = Gender.Male,
                    Account = new Account
                    {
                        IsBlocked = false,
                        AuthLocal = new AuthLocal
                        {
                            PasswordHash = PasswordHasher.GenerateHash("1qaz1qaz", saltDef),
                            Salt = saltDef,
                            Email = "admin@gmail.com",
                            EmailConfirmed = true,
                        },
                        AccountRoles = new[]
                        {
                            new AccountRole
                            {
                                RoleId = Enums.Role.Admin,
                            },
                        },
                    },
                },

                new User
                {
                    Name = "User",
                    Email = "user@gmail.com",
                    Phone = "+380974293580",
                    Birthday = DateTime.Parse("2000-01-01"),
                    Gender = Gender.Male,
                    Account = new Account
                    {
                        IsBlocked = false,
                        AuthLocal = new AuthLocal
                        {
                            PasswordHash = PasswordHasher.GenerateHash("1qaz1qaz", saltDef),
                            Salt = saltDef,
                            Email = "user@gmail.com",
                            EmailConfirmed = true,
                        },
                        AccountRoles = new[]
                        {
                            new AccountRole
                            {
                                RoleId = Enums.Role.User,
                            },
                        },
                    },
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

            var emailMessages = new[]
            {
                new NotificationTemplate { Id = NotificationProfile.BlockedUser, Title = "BlockedUser", Subject = "Your account was blocked", Message = "Dear (UserName), your account was blocked for some reason!" },
                new NotificationTemplate { Id = NotificationProfile.CreateEventVerification, Title = "CreateEventVerification", Subject = "Approve your recurrent event!", Message = "Follow the <a href='(link)'>link</a> to create the recurrent event." },
                new NotificationTemplate { Id = NotificationProfile.EventCreated, Title = "EventCreated", Subject = "New event for you!", Message = "The <a href='(link)'>event</a> was created which could interested you." },
                new NotificationTemplate { Id = NotificationProfile.EventStatusCanceled, Title = "EventStatusCanceled", Subject = "The event you have been joined was canceled",  Message = "Dear (UserName), the event you have been joined was CANCELED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Id = NotificationProfile.EventStatusBlocked, Title = "EventStatusBlocked", Subject = "The event you have been joined was blocked",  Message = "Dear (UserName), the event you have been joined was BLOCKED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Id = NotificationProfile.EventStatusActivated, Title = "EventStatusActivated", Subject = "The event you have been joined was activated",  Message = "Dear (UserName), the event you have been joined was ACTIVATED. The reason is: (reason) \"<a href='(link)'>(title)</a>\"" },
                new NotificationTemplate { Id = NotificationProfile.ParticipationApproved, Title = "ParticipationApproved", Subject = "Approving participation", Message = "Dear (UserName), you have been approved to join to this event. To check it, please, visit \"<a href='(link)'>EventExpress</a>\"" },
                new NotificationTemplate { Id = NotificationProfile.ParticipationDenied, Title = "ParticipationDenied", Subject = "Denying participation", Message = "Dear (UserName), you have been denied to join to this event. To check it, please, visit \"<a href='(link)'>EventExpress</a>\"" },
                new NotificationTemplate { Id = NotificationProfile.RegisterVerification, Title = "RegisterVerification", Subject = "EventExpress registration", Message = "For confirm your email please follow the <a href='(link)'>link</a>" },
                new NotificationTemplate { Id = NotificationProfile.UnblockedUser, Title = "UnblockedUser", Subject = "Your account was Unblocked", Message = "Dear (UserName), congratulations, your account was Unblocked, so you can come back and enjoy spending your time in EventsExpress" },
            };

            dbContext.NotificationTemplates.AddRange(emailMessages);

            dbContext.SaveChanges();
        }
    }
}
