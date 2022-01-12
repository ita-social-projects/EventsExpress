using HotChocolate.Data.Sorting;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.GraphQL.SortInputTypes
{
    public class PointSortType : SortInputType<Point>
    {
        protected override void Configure(ISortInputTypeDescriptor<Point> descriptor)
        {
            descriptor.Ignore(f => f.Area);
            descriptor.Ignore(f => f.Boundary);
            descriptor.Ignore(f => f.BoundaryDimension);
            descriptor.Ignore(f => f.Centroid);
            descriptor.Field(f => f.Coordinate);
            descriptor.Ignore(f => f.Coordinates);
            descriptor.Ignore(f => f.CoordinateSequence);
            descriptor.Ignore(f => f.Dimension);
            descriptor.Ignore(f => f.Envelope);
            descriptor.Ignore(f => f.EnvelopeInternal);
            descriptor.Ignore(f => f.Factory);
            descriptor.Field(f => f.GeometryType);
            descriptor.Ignore(f => f.InteriorPoint);
            descriptor.Field(f => f.IsEmpty);
            descriptor.Field(f => f.IsRectangle);
            descriptor.Field(f => f.IsSimple);
            descriptor.Field(f => f.IsValid);
            descriptor.Field(f => f.Length);
            descriptor.Field(f => f.M);
            descriptor.Field(f => f.NumGeometries);
            descriptor.Field(f => f.NumPoints);
            descriptor.Field(f => f.OgcGeometryType);
            descriptor.Field(f => f.PointOnSurface);
            descriptor.Field(f => f.PrecisionModel);
            descriptor.Field(f => f.SRID);
            descriptor.Ignore(f => f.UserData);
            descriptor.Field(f => f.X);
            descriptor.Field(f => f.Y);
            descriptor.Field(f => f.Z);
        }
    }
}
