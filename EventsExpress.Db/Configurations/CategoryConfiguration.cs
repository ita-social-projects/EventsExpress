using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                .IsRequired();
            builder.HasOne(c => c.CategoryGroup)
                .WithMany(cg => cg.Categories)
                .HasForeignKey(c => c.CategoryGroupId);
        }
    }
}
