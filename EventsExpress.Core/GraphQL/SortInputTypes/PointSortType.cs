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
            descriptor.Ignore(f => f.Coordinate);
            descriptor.Ignore(f => f.Coordinates);
            descriptor.Ignore(f => f.CoordinateSequence);
            descriptor.Ignore(f => f.Dimension);
            descriptor.Ignore(f => f.Envelope);
            descriptor.Ignore(f => f.EnvelopeInternal);
            descriptor.Ignore(f => f.Factory);
            descriptor.Ignore(f => f.GeometryType);
            descriptor.Ignore(f => f.InteriorPoint);
            descriptor.Ignore(f => f.IsEmpty);
            descriptor.Ignore(f => f.IsRectangle);
            descriptor.Ignore(f => f.IsSimple);
            descriptor.Ignore(f => f.IsValid);
            descriptor.Ignore(f => f.Length);
            descriptor.Ignore(f => f.M);
            descriptor.Ignore(f => f.NumGeometries);
            descriptor.Ignore(f => f.NumPoints);
            descriptor.Ignore(f => f.OgcGeometryType);
            descriptor.Ignore(f => f.PointOnSurface);
            descriptor.Ignore(f => f.PrecisionModel);
            descriptor.Ignore(f => f.SRID);
            descriptor.Ignore(f => f.UserData);
            descriptor.Field(f => f.X);
            descriptor.Field(f => f.Y);
            descriptor.Field(f => f.Z);
        }
    }
}
