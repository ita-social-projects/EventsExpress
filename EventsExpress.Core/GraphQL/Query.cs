using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Core.GraphQL.IServices;
using EventsExpress.Db.Entities;
using HotChocolate;
using HotChocolate.Data;

namespace EventsExpress.Core.GraphQL
{
    public class Query
    {
        private readonly IEventService eventService;

        public Query(IEventService eventService)
        {
            this.eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
        }

        [UseApplicationDbContext]
        [UseProjection]
        public async Task<IQueryable<Event>> GetEvents()
        {
            return await eventService.GetEvents();
        }
    }
}
