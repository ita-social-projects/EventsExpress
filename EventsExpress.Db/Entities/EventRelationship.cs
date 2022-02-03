using System;
using EventsExpress.Db.EF;

namespace EventsExpress.Db.Entities;

public abstract class EventRelationship
{
    public string Discriminator { get; set; }

    [Track]
    public Guid UserFromId { get; set; }

    public virtual User UserFrom { get; set; }

    [Track]
    public Guid EventId { get; set; }

    public Event Event { get; set; }
}
