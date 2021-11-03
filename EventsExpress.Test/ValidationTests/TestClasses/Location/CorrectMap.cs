namespace EventsExpress.Test.ValidationTests.TestClasses.Location
{
    using System.Collections;
    using EventsExpress.Db.Enums;
    using EventsExpress.ViewModels.Base;

    public class CorrectMap : IEnumerable
    {
        private readonly double[,] pointArr = new double[,]
                         {
                            { 1, 1 },
                            { -2.2, -1 },
                            { 0, 0 },
                            { -104, 10 },
                            { 10, 109 },
                         };

        private LocationType type = LocationType.Map;

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < pointArr.GetLength(0); i++)
            {
                yield return new object[] { new MapViewModel { Type = type, Latitude = pointArr[i, 0], Longitude = pointArr[i, 1] } };
            }
        }
    }
}
