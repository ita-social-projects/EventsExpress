using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class RelationshipConfiguration : IEntityTypeConfiguration<Relationship>
    {
        public void Configure(EntityTypeBuilder<Relationship> builder)
        {
            builder.HasOne(r => r.UserFrom)
                .WithMany(u => u.Relationships)
                .HasForeignKey(r => r.UserFromId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
