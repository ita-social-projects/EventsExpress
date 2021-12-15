using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations
{
    using System;

    public class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
    {
        public static CategoryGroup[] CategoryGroups { get; } =
        {
            new CategoryGroup { Id = Guid.NewGuid(), Title = "Art&Craft" },
            new CategoryGroup { Id = Guid.NewGuid(), Title = "Education&Training" },
            new CategoryGroup { Id = Guid.NewGuid(), Title = "Wellness, Health&Fitness" },
        };

        public void Configure(EntityTypeBuilder<CategoryGroup> builder)
        {
            builder.HasData(CategoryGroups);
        }
    }
}
