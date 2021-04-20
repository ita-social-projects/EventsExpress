using System;
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

            dbContext.SaveChanges();
        }
    }
}
