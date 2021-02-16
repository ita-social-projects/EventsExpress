using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace EventsExpress.Db.EF
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Rate> Rates { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Relationship> Relationships { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<EventOwner> EventOwners { get; set; }

        public DbSet<EventLocation> EventLocations { get; set; }

        public DbSet<EventSchedule> EventSchedules { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ChatRoom> ChatRoom { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<EventStatusHistory> EventStatusHistory { get; set; }

        public DbSet<UserEventInventory> UserEventInventories { get; set; }

        public DbSet<UserEvent> UserEvent { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<UnitOfMeasuring> UnitOfMeasurings { get; set; }

        public DbSet<ChangeInfo> ChangeInfos { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<UserNotificationType> UserNotificationTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // user config
            modelBuilder.Entity<User>()
                .Property(u => u.Birthday).HasColumnType("date");
            modelBuilder.Entity<User>()
                .Property(u => u.Salt).HasMaxLength(16);

            modelBuilder.Entity<UnitOfMeasuring>()
                .HasIndex(u => new { u.UnitName, u.ShortName }).IsUnique().HasFilter("IsDeleted = 0");

            // user-event many-to-many configs
            // user as visitor
            modelBuilder.Entity<UserEvent>()
                .HasKey(t => new { t.UserId, t.EventId });
            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.EventsToVisit)
                .HasForeignKey(ue => ue.UserId);
            modelBuilder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Visitors)
                .HasForeignKey(ue => ue.EventId);

            modelBuilder.Entity<EventOwner>()
                .HasKey(c => new { c.UserId, c.EventId });
            modelBuilder.Entity<EventOwner>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ue => ue.UserId);
            modelBuilder.Entity<EventOwner>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Owners)
                .HasForeignKey(ue => ue.EventId);
            modelBuilder.Entity<EventOwner>()
                .HasIndex(p => new { p.UserId, p.EventId });

            // user as owner
            modelBuilder.Entity<Event>()
                .Property(u => u.DateFrom).HasColumnType("date");
            modelBuilder.Entity<Event>()
                .Property(u => u.DateTo).HasColumnType("date");

            // rates config
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.UserFrom)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserFromId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Rate>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Rates)
                .HasForeignKey(r => r.EventId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Relationship>()
                .HasOne(r => r.UserFrom)
                .WithMany(u => u.Relationships)
                .HasForeignKey(r => r.UserFromId).OnDelete(DeleteBehavior.Restrict);

            // user-category many-to-many
            modelBuilder.Entity<UserCategory>()
                .HasKey(t => new { t.UserId, t.CategoryId });
            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(uc => uc.UserId);
            modelBuilder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.CategoryId);

            // event-category many-to-many
            modelBuilder.Entity<EventCategory>()
                .HasKey(t => new { t.EventId, t.CategoryId });
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(ec => ec.EventId);
            modelBuilder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(uc => uc.CategoryId);

            // EventStatusHistory config
            modelBuilder.Entity<EventStatusHistory>()
                .HasOne(esh => esh.User)
                .WithMany(u => u.ChangedStatusEvents);
            modelBuilder.Entity<EventStatusHistory>()
                .HasOne(esh => esh.Event)
                .WithMany(e => e.StatusHistory);

            // category config
            modelBuilder.Entity<Category>()
                .Property(c => c.Name).IsRequired();

            // comment config
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Parent).WithMany(prop => prop.Children).HasForeignKey(c => c.CommentsId);

            // event config
            modelBuilder.Entity<Event>()
                .Property(c => c.MaxParticipants).HasDefaultValue(int.MaxValue);

            // inventory config
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Inventories)
                .HasForeignKey(i => i.EventId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Inventory>()
                .HasOne(i => i.UnitOfMeasuring)
                .WithMany(u => u.Inventories)
                .HasForeignKey(i => i.UnitOfMeasuringId).OnDelete(DeleteBehavior.Restrict);

            // userevent-inventory many-to-many
            modelBuilder.Entity<UserEventInventory>()
                .HasKey(t => new { t.InventoryId, t.UserId, t.EventId });
            modelBuilder.Entity<UserEventInventory>()
                .HasOne(uei => uei.UserEvent)
                .WithMany(ue => ue.Inventories)
                .HasForeignKey(uei => new { uei.UserId, uei.EventId }).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserEventInventory>()
                .HasOne(uei => uei.Inventory)
                .WithMany(i => i.UserEventInventories)
                .HasForeignKey(uei => uei.InventoryId).OnDelete(DeleteBehavior.Restrict);

            // user-notification
            modelBuilder.Entity<UserNotificationType>()
              .HasKey(t => new { t.UserId, t.NotificationTypeId });
            modelBuilder.Entity<UserNotificationType>()
                .HasOne(ec => ec.User)
                .WithMany(e => e.NotificationTypes)
                .HasForeignKey(ec => ec.UserId);
            modelBuilder.Entity<UserNotificationType>()
                .HasOne(ec => ec.NotificationType)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.NotificationTypeId);

            // notification type
            modelBuilder.Entity<NotificationType>()
                .HasData(new[]
                {
                    new NotificationType { Id = NotificationChange.OwnEvent, Name = "Own Event Change" },
                    new NotificationType { Id = NotificationChange.Profile, Name = "Profile Change" },
                    new NotificationType { Id = NotificationChange.VisitedEvent, Name = "Visited Event Change" },
                });
            modelBuilder.Entity<NotificationType>()
            .HasKey(c => c.Id);

            modelBuilder.Entity<NotificationType>()
                .Property(c => c.Id)
                .ValueGeneratedNever();
            modelBuilder.Entity<NotificationType>()
                .Property(c => c.Name)
                .IsRequired();
        }

        public void SaveTracks()
        {
            var trackEntities = ChangeTracker.Entries()
                 .Where(p =>
                     (p.State == EntityState.Modified || p.State == EntityState.Added || p.State == EntityState.Deleted)
                     && p.Entity.GetType().CustomAttributes.Any(x => x.AttributeType == typeof(TrackAttribute))).ToList();
            var now = DateTime.UtcNow;
            foreach (var change in trackEntities)
            {
                var entityKeyDictionary = new Dictionary<string, string>();
                var keyNames = this.Model.FindEntityType(change.Entity.GetType()).FindPrimaryKey().Properties.Select(x => x.Name);

                foreach (var k in keyNames)
                {
                    entityKeyDictionary.Add(k, change.CurrentValues[k].ToString());
                }

                var changeInfo = new ChangeInfo
                {
                    PropertyChangesText = JsonConvert.SerializeObject(LogPropertChanges(change)),
                    EntityName = change.Entity.GetType().Name,
                    Time = now,
                    ChangesType = MapEntityStateToChangeType(change.State),
                    UserId = CurrentUserId(),
                    EntityKeys = JsonConvert.SerializeObject(entityKeyDictionary),
                };

                ChangeInfos.Add(changeInfo);
            }
        }

        private List<PropertyChangeInfo> LogPropertChanges(EntityEntry change)
        {
            var propChangeInfos = new List<PropertyChangeInfo>();
            var trackedProps = change.Entity.GetType().GetProperties().Where(x => x.CustomAttributes.Any(z => z.AttributeType == typeof(TrackAttribute))).ToList();

            foreach (var prop in trackedProps)
            {
                switch (change.State)
                {
                    case EntityState.Modified:
                        {
                            var newValue = change.CurrentValues[prop.Name]?.ToString();
                            var oldValue = change.OriginalValues[prop.Name]?.ToString();

                            if (oldValue != newValue)
                            {
                                propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, NewValue = newValue, OldValue = oldValue });
                            }
                        }

                        break;
                    case EntityState.Added:
                        {
                            var newValue = change.CurrentValues[prop.Name]?.ToString();
                            propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, NewValue = newValue });
                        }

                        break;
                    case EntityState.Deleted:
                        {
                            var oldValue = change.OriginalValues[prop.Name].ToString();
                            propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, OldValue = oldValue });
                        }

                        break;

                    default:
                        break;
                }
            }

            return propChangeInfos;
        }

        private ChangesType MapEntityStateToChangeType(EntityState state)
        {
            switch (state)
            {
                case EntityState.Added:
                    return ChangesType.Create;

                case EntityState.Modified:
                    return ChangesType.Edit;

                case EntityState.Deleted:
                    return ChangesType.Delete;

                default:
                    return ChangesType.Undefined;
            }
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            SaveTracks();

            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            SaveTracks();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private Guid CurrentUserId() =>
           _httpContextAccessor.HttpContext != null ?
            new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value) :
            Guid.Empty;
    }
}
