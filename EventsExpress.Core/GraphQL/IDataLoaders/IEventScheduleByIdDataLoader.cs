using System;
using EventsExpress.Db.Entities;
using GreenDonut;

namespace EventsExpress.Core.GraphQL.IDataLoaders
{
    public interface IEventScheduleByIdDataLoader : IDataLoader<Guid, EventSchedule>
    {
    }
}
