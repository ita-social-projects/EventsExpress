using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<EventSchedule> EventSchedules { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Comments> Comments { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<ChatRoom> ChatRoom { get; set; }

        public DbSet<Photo> Photos { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<EventStatusHistory> EventStatusHistory { get; set; }

        public DbSet<UserEventInventory> UserEventInventories { get; set; }

        public DbSet<UserEvent> UserEvent { get; set; }

        public DbSet<Inventory> Inventories { get; set; }

        public DbSet<UnitOfMeasuring> UnitOfMeasurings { get; set; }

        public DbSet<ChangeInfo> ChangeInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // user config
            builder.Entity<User>()
                .Property(u => u.Birthday).HasColumnType("date");

            // user-event many-to-many configs
            // user as visitor
            builder.Entity<UserEvent>()
                .HasKey(t => new { t.UserId, t.EventId });
            builder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.EventsToVisit)
                .HasForeignKey(ue => ue.UserId);
            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Visitors)
                .HasForeignKey(ue => ue.EventId);

            builder.Entity<EventOwner>()
                .HasKey(c => new { c.UserId, c.EventId });
            builder.Entity<EventOwner>()
                .HasOne(ue => ue.User)
                .WithMany(u => u.Events)
                .HasForeignKey(ue => ue.UserId);
            builder.Entity<EventOwner>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.Owners)
                .HasForeignKey(ue => ue.EventId);
            builder.Entity<EventOwner>()
                .HasIndex(p => new { p.UserId, p.EventId });

            // user as owner
            builder.Entity<Event>()
                .Property(u => u.DateFrom).HasColumnType("date");
            builder.Entity<Event>()
                .Property(u => u.DateTo).HasColumnType("date");

            // rates config
            builder.Entity<Rate>()
                .HasOne(r => r.UserFrom)
                .WithMany(u => u.Rates)
                .HasForeignKey(r => r.UserFromId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Rate>()
                .HasOne(r => r.Event)
                .WithMany(e => e.Rates)
                .HasForeignKey(r => r.EventId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Relationship>()
                .HasOne(r => r.UserFrom)
                .WithMany(u => u.Relationships)
                .HasForeignKey(r => r.UserFromId).OnDelete(DeleteBehavior.Restrict);

            // user-category many-to-many
            builder.Entity<UserCategory>()
                .HasKey(t => new { t.UserId, t.CategoryId });
            builder.Entity<UserCategory>()
                .HasOne(uc => uc.User)
                .WithMany(u => u.Categories)
                .HasForeignKey(uc => uc.UserId);
            builder.Entity<UserCategory>()
                .HasOne(uc => uc.Category)
                .WithMany(c => c.Users)
                .HasForeignKey(uc => uc.CategoryId);

            // event-category many-to-many
            builder.Entity<EventCategory>()
                .HasKey(t => new { t.EventId, t.CategoryId });
            builder.Entity<EventCategory>()
                .HasOne(ec => ec.Event)
                .WithMany(e => e.Categories)
                .HasForeignKey(ec => ec.EventId);
            builder.Entity<EventCategory>()
                .HasOne(ec => ec.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(uc => uc.CategoryId);

            // EventStatusHistory config
            builder.Entity<EventStatusHistory>()
                .HasOne(esh => esh.User)
                .WithMany(u => u.ChangedStatusEvents);
            builder.Entity<EventStatusHistory>()
                .HasOne(esh => esh.Event)
                .WithMany(e => e.StatusHistory);

            // category config
            builder.Entity<Category>()
                .Property(c => c.Name).IsRequired();

            // country config
            builder.Entity<Country>()
                .Property(c => c.Name).IsRequired();
            builder.Entity<Country>()
                .HasIndex(c => c.Name).IsUnique();

            // city config
            builder.Entity<City>()
                .Property(c => c.Name).IsRequired();

            // comment config
            builder.Entity<Comments>()
                .HasOne(c => c.Parent).WithMany(prop => prop.Children).HasForeignKey(c => c.CommentsId);

            // event config
            builder.Entity<Event>()
                .Property(c => c.MaxParticipants).HasDefaultValue(int.MaxValue);

            // inventory config
            builder.Entity<Inventory>()
                .HasOne(i => i.Event)
                .WithMany(e => e.Inventories)
                .HasForeignKey(i => i.EventId).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Inventory>()
                .HasOne(i => i.UnitOfMeasuring)
                .WithMany(u => u.Inventories)
                .HasForeignKey(i => i.UnitOfMeasuringId).OnDelete(DeleteBehavior.Restrict);

            // userevent-inventory many-to-many
            builder.Entity<UserEventInventory>()
                .HasKey(t => new { t.InventoryId, t.UserId, t.EventId });
            builder.Entity<UserEventInventory>()
                .HasOne(uei => uei.UserEvent)
                .WithMany(ue => ue.Inventories)
                .HasForeignKey(uei => new { uei.UserId, uei.EventId }).OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserEventInventory>()
                .HasOne(uei => uei.Inventory)
                .WithMany(i => i.UserEventInventories)
                .HasForeignKey(uei => uei.InventoryId).OnDelete(DeleteBehavior.Restrict);
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
                var entitKeyDictionary = new Dictionary<string, string>();
                var changeInfo = new ChangeInfo();
                var propChangeInfos = new List<PropertyChangeInfo>();
                var entityType = change.Entity.GetType();
                var keyNames = this.Model.FindEntityType(entityType).FindPrimaryKey().Properties.Select(x => x.Name);

                foreach (var k in keyNames)
                {
                    entitKeyDictionary.Add(k, change.CurrentValues[k].ToString());
                }

                var entityKeys = Newtonsoft.Json.JsonConvert.SerializeObject(entitKeyDictionary);
                var trackedProps = entityType.GetProperties().Where(x => x.CustomAttributes.Any(z => z.AttributeType == typeof(TrackAttribute))).ToList();

                var changesType = Enums.ChangesType.Undefined;

                if (change.State == EntityState.Added)
                {
                    changesType = Enums.ChangesType.Create;
                    foreach (var prop in trackedProps)
                    {
                        var newValue = change.CurrentValues[prop.Name]?.ToString();
                        propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, NewValue = newValue });
                    }

                }
                else if (change.State == EntityState.Modified)
                {
                    changesType = Enums.ChangesType.Edit;
                    foreach (var prop in trackedProps)
                    {
                        var newValue = change.CurrentValues[prop.Name].ToString();
                        var oldValue = change.OriginalValues[prop.Name].ToString();

                        if (oldValue != newValue)
                        {
                            propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, NewValue = newValue, OldValue = oldValue });
                        }
                    }
                }
                else if (change.State == EntityState.Deleted)
                {
                    changesType = Enums.ChangesType.Delete;
                    foreach (var prop in trackedProps)
                    {
                        var oldValue = change.OriginalValues[prop.Name].ToString();

                        propChangeInfos.Add(new PropertyChangeInfo { Name = prop.Name, OldValue = oldValue });
                    }
                }

                var text = Newtonsoft.Json.JsonConvert.SerializeObject(propChangeInfos);
                changeInfo.PropertyChangesText = text;
                changeInfo.EntityName = entityType.Name;
                changeInfo.Time = now;
                changeInfo.ChangesType = changesType;
                changeInfo.UserId = _httpContextAccessor.HttpContext != null ? new Guid(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Name).Value) : Guid.Empty;
                changeInfo.EntityKeys = entityKeys;
                ChangeInfos.Add(changeInfo);
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
    }
}