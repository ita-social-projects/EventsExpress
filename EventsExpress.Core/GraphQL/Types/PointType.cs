namespace EventsExpress.Core.GraphQL.Types
{
    using HotChocolate.Types;
    using NetTopologySuite.Geometries;

    public class PointType : ObjectType<Point>
    {
        protected override void Configure(IObjectTypeDescriptor<Point> descriptor)
        {
            descriptor.Field(f => f.X);
            descriptor.Field(f => f.Y);

            descriptor.Field(f => f.Z);
            descriptor.Field(f => f.Area);
            descriptor.Field(f => f.Boundary);
            descriptor.Field(f => f.BoundaryDimension);
            descriptor.Field(f => f.Centroid);
            descriptor.Field(f => f.Coordinate);
            descriptor.Field(f => f.Coordinates);
            descriptor.Field(f => f.CoordinateSequence);
            descriptor.Field(f => f.Dimension);
            descriptor.Field(f => f.Envelope);
            descriptor.Field(f => f.EnvelopeInternal);
            descriptor.Field(f => f.Factory);
            descriptor.Field(f => f.GeometryType);
            descriptor.Field(f => f.InteriorPoint);
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
            descriptor.Field(f => f.UserData);
        }
    }
}
