using System;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations;

public class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
{
    public static CategoryGroup ArtAndCraftGroup { get; } = new () { Id = Guid.NewGuid(), Title = "Art&Craft" };

    public static CategoryGroup EducationAndTraining { get; } = new () { Id = Guid.NewGuid(), Title = "Education&Training" };

    public static CategoryGroup WellnessHealthAndFitness { get; } = new () { Id = Guid.NewGuid(), Title = "Wellness, Health&Fitness" };

    public static CategoryGroup[] CategoryGroups { get; } =
    {
        ArtAndCraftGroup,
        EducationAndTraining,
        WellnessHealthAndFitness,
    };

    public void Configure(EntityTypeBuilder<CategoryGroup> builder)
    {
        builder.HasData(CategoryGroups);
    }
}
