namespace EventsExpress.Db.Configurations
{
    using EventsExpress.Db.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MultiEventStatusConfiguration : IEntityTypeConfiguration<MultiEventStatus>
    {
        public void Configure(EntityTypeBuilder<MultiEventStatus> builder)
        {
            builder.HasOne(e => e.ParentEvent)
              .WithMany(ec => ec.ChildEvents)
              .HasForeignKey(e => e.ParentId)
             .OnDelete(DeleteBehavior.ClientCascade);
            builder.HasOne(e => e.ChildEvent)
              .WithMany(ec => ec.ParentEvents)
              .HasForeignKey(e => e.ChildId)
              .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
