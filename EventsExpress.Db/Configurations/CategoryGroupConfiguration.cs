using System;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configurations;

public class CategoryGroupConfiguration : IEntityTypeConfiguration<CategoryGroup>
{
    public static CategoryGroup ArtAndCraftGroup { get; } = new () { Id = new Guid("88B791A5-6CE3-4B50-80AE-65572991F676"), Title = "Art&Craft" };

    public static CategoryGroup EducationAndTraining { get; } = new () { Id = new Guid("D11D77E5-818D-41B4-AFFD-780A1991A16C"), Title = "Education&Training" };

    public static CategoryGroup WellnessHealthAndFitness { get; } = new () { Id = new Guid("78ED6EE2-9D5A-4802-ACED-B3284E948A83"), Title = "Wellness, Health&Fitness" };

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
