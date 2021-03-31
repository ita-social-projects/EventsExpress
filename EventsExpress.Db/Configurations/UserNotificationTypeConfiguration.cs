using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EventsExpress.Db.Configuration
{
    public class UserNotificationTypeConfiguration : IEntityTypeConfiguration<UserNotificationType>
    {
        public void Configure(EntityTypeBuilder<UserNotificationType> builder)
        {
            builder.HasKey(t => new { t.UserId, t.NotificationTypeId });
            builder.HasOne(ec => ec.User)
                .WithMany(e => e.NotificationTypes)
                .HasForeignKey(ec => ec.UserId);
            builder.HasOne(ec => ec.NotificationType)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.NotificationTypeId);
        }
    }
}
