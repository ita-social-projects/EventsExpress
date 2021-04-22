using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ChatRoom> ChatRoom { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<EventStatusHistory> EventStatusHistory { get; set; }

        public DbSet<UserEventInventory> UserEventInventories { get; set; }

        public DbSet<UserEvent> UserEvent { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<UnitOfMeasuring> UnitOfMeasurings { get; set; }

        public DbSet<ChangeInfo> ChangeInfos { get; set; }

        public DbSet<NotificationType> NotificationTypes { get; set; }

        public DbSet<UserNotificationType> UserNotificationTypes { get; set; }

        public DbSet<ContactAdmin> ContactAdmin { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // user config
            modelBuilder.Entity<User>()
                .Property(u => u.Birthday).HasColumnType("date");
            modelBuilder.Entity<User>()
                .Property(u => u.Salt).HasMaxLength(16);
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
