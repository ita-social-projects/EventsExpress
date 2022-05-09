namespace EventsExpress.Core.GraphQL.Types
{
    public class CustomPoint
    {
        public CustomPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double SRID { get; set; }
    }
}
