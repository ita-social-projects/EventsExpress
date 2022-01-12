﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.GraphQL.Extensions;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using GreenDonut;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Resolvers;
using HotChocolate.Types;
using HotChocolate.Types.Spatial;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventType : ObjectType<Event>
    {
        protected override void Configure(IObjectTypeDescriptor<Event> descriptor)
        {
            descriptor.Field(f => f.Title);

            descriptor.Field(f => f.Description);

            descriptor.Field(f => f.DateFrom);

            descriptor.Field(f => f.DateTo);

            descriptor.Field(f => f.IsPublic);

            descriptor.Field(f => f.MaxParticipants);

            descriptor.Field(f => f.EventLocationId);

            descriptor.Field(f => f.EventSchedule);

            descriptor.Field(f => f.EventLocation);

            descriptor.Field(f => f.Owners);

            descriptor.Field(f => f.Visitors);

            descriptor.Field(f => f.Categories);

            descriptor.Field(f => f.Rates);

            descriptor.Field(f => f.Inventories);

            descriptor.Field(f => f.StatusHistory);
        }
    }
}