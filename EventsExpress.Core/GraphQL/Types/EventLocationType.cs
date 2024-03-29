﻿using EventsExpress.Db.EF;
using HotChocolate.Types;
using NetTopologySuite.Geometries;
using EventLocation = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventLocationType : ObjectType<EventLocation>
    {
        protected override void Configure(IObjectTypeDescriptor<EventLocation> descriptor)
        {
            descriptor
                .Field(f => f.Point)
                .Description("Point coordinates")
                .UseDbContext<AppDbContext>()
                .Resolve(resolver =>
                {
                    Point point = resolver.Parent<EventLocation>()?.Point;

                    if (point != null)
                    {
                        return new CustomPoint(point.X, point.Y) { SRID = 4326 };
                    }

                    return null;
                });

            descriptor.Field(f => f.OnlineMeeting);
            descriptor.Field(f => f.Type);
        }
    }
}
