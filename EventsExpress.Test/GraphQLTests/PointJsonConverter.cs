using System;
using NetTopologySuite.Geometries;
using Newtonsoft.Json.Converters;

namespace EventsExpress.Test.GraphQLTests
{
    public class PointJsonConverter : CustomCreationConverter<Point>
    {
        public override Point Create(Type objectType)
        {
            return new Point(0, 0);
        }
    }
}
