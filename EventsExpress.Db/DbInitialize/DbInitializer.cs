using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.Configurations;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;
using Location = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Db.DbInitialize
{
    [ExcludeFromCodeCoverage]
    public static class DbInitializer
    {
        public static void Seed(AppDbContext dbContext, IPasswordHasher passwordHasher)
        {
            dbContext.Database.EnsureCreated();
            SeedUsers(dbContext, passwordHasher);
            SeedCategories(dbContext);
            SeedUnitsOfMeasuring(dbContext);
            dbContext.SaveChanges();
        }

        private static void SeedUsers(AppDbContext dbContext, IPasswordHasher passwordHasher)
        {
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
                    LocationId = Guid.NewGuid(),
                    Location = new Location
                    {
                        Type = LocationType.Map,
                        Point = Point.Empty,
                    },
                },
            };

            dbContext.Users.AddRange(users);
        }

        private static void SeedCategories(AppDbContext dbContext)
        {
            if (dbContext.Categories.Any())
            {
                return;
            }

            var categories = new Category[]
            {
                new Category { Name = "Drawing", CategoryGroupId = CategoryGroupConfiguration.ArtAndCraftGroup.Id },
                new Category { Name = "Pottery", CategoryGroupId = CategoryGroupConfiguration.ArtAndCraftGroup.Id },
                new Category { Name = "Self-education", CategoryGroupId = CategoryGroupConfiguration.EducationAndTraining.Id },
                new Category { Name = "Public Speaking", CategoryGroupId = CategoryGroupConfiguration.EducationAndTraining.Id },
                new Category { Name = "Book Club", CategoryGroupId = CategoryGroupConfiguration.EducationAndTraining.Id },
                new Category { Name = "Climbing", CategoryGroupId = CategoryGroupConfiguration.WellnessHealthAndFitness.Id },
                new Category { Name = "Volleyball", CategoryGroupId = CategoryGroupConfiguration.WellnessHealthAndFitness.Id },
                new Category { Name = "Football", CategoryGroupId = CategoryGroupConfiguration.WellnessHealthAndFitness.Id },
            };

            dbContext.Categories.AddRange(categories);
        }

        private static void SeedUnitsOfMeasuring(AppDbContext dbContext)
        {
            if (dbContext.UnitOfMeasurings.Any())
            {
                return;
            }

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
        }
    }
}
