using EventsExpress.Db.Entities;
using HotChocolate.Types;
using HotChocolate.Types.Spatial;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.GraphQL.Types
{
    public class EventLocationType
    {
        protected void Configure(IObjectTypeDescriptor<EventLocation> descriptor)
        {
            descriptor.Field(f => f.Point).Type<GeoJsonPointType>();
            descriptor.Field(f => f.OnlineMeeting).Type<UrlType>();
            descriptor.Field(f => f.Type).Type<LocationTypeEnumType>();
        }
    }
}
