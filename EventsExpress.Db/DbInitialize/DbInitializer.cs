using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;

namespace EventsExpress.Db.DbInitialize
{
    [ExcludeFromCodeCoverage]
    public static class DbInitializer
    {
        public static void Seed(AppDbContext dbContext, IPasswordHasher passwordHasher)
        {
            dbContext.Database.EnsureCreated();

            // Look for any users
            if (dbContext.Users.Any())
            {
                return; // DB has been seeded
            }

            var saltDef = passwordHasher.GenerateSalt();
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
                            PasswordHash = passwordHasher.GenerateHash("1qaz1qaz", saltDef),
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
                            PasswordHash = passwordHasher.GenerateHash("1qaz1qaz", saltDef),
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

            // var categories = new Category[]
            // {
            //     new Category { Name = "Sea" },
            //     new Category { Name = "Mount" },
            //     new Category { Name = "Summer" },
            //     new Category { Name = "Golf" },
            //     new Category { Name = "Team-Building" },
            //     new Category { Name = "Swimming" },
            //     new Category { Name = "Gaming" },
            //     new Category { Name = "Fishing" },
            //     new Category { Name = "Trips" },
            //     new Category { Name = "Meeting" },
            //     new Category { Name = "Sport" },
            // };

            // dbContext.Categories.AddRange(categories);
            var categoryGroups = new CategoryGroup[]
            {
                new CategoryGroup { Title = "Art&Craft" },
                new CategoryGroup { Title = "Wellness, Health&Fitness" },
            };

            dbContext.CategoryGroups.AddRange(categoryGroups);

            // let's check those groups of categories
            var categories = new Category[]
            {
                new Category { Name = "Drawing", CategoryGroup = categoryGroups[0] },
                new Category { Name = "Pottery", CategoryGroup = categoryGroups[0] },
                new Category { Name = "Climbing", CategoryGroup = categoryGroups[1] },
                new Category { Name = "Football", CategoryGroup = categoryGroups[1] },
                new Category { Name = "Volleyball", CategoryGroup = categoryGroups[1] },
            };

            dbContext.Categories.AddRange(categories);

            var unitsOfMeasuring = new UnitOfMeasuring[]
            {
                new UnitOfMeasuring
                {
                    UnitName = "Kilogram",
                    ShortName = "kg",
                    IsDeleted = false,
                    Category = new CategoryOfMeasuring
                    {
                        CategoryName = "Weight",
                    },
                },
                new UnitOfMeasuring
                {
                    UnitName = "Meter",
                    ShortName = "m",
                    IsDeleted = false,
                    Category = new CategoryOfMeasuring
                    {
                        CategoryName = "Length",
                    },
                },
                new UnitOfMeasuring
                {
                    UnitName = "Liter",
                    ShortName = "l",
                    IsDeleted = false,
                    Category = new CategoryOfMeasuring
                    {
                        CategoryName = "Liquids",
                    },
                },
                new UnitOfMeasuring
                {
                    UnitName = "Piece",
                    ShortName = "pc",
                    IsDeleted = false,
                    Category = new CategoryOfMeasuring
                    {
                        CategoryName = "Quantity",
                    },
                },
            };

            dbContext.UnitOfMeasurings.AddRange(unitsOfMeasuring);

            var emailMessages = new[]
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
            };

            dbContext.NotificationTemplates.AddRange(emailMessages);

            dbContext.SaveChanges();
        }
    }
}
