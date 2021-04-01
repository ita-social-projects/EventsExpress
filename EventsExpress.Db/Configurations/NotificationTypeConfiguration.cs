using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class NotificationTypeConfiguration : IEntityTypeConfiguration<NotificationType>
    {
        public void Configure(EntityTypeBuilder<NotificationType> builder)
        {
            builder.HasData(new[]
                {
                    new NotificationType { Id = NotificationChange.Profile, Name = "Profile Change" },
                    new NotificationType { Id = NotificationChange.OwnEvent, Name = "Own Event Change" },
                    new NotificationType { Id = NotificationChange.VisitedEvent, Name = "Visited Event Change" },
                });
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .ValueGeneratedNever();
            builder.Property(c => c.Name)
                .IsRequired();
        }
    }
}
