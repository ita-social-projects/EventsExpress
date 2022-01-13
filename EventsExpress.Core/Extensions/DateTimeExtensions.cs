using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddDateUnit(Periodicity periodicity, int frequency, DateTime date)
        {
            switch (periodicity)
            {
                case Periodicity.Daily:
                    return date.AddDays(frequency);
                case Periodicity.Weekly:
                    return date.AddDays(frequency * 7);
                case Periodicity.Monthly:
                    return date.AddMonths(frequency);
                case Periodicity.Yearly:
                    return date.AddYears(frequency);
                default:
                    return date;
            }
        }

        public static int GetDifferenceInYears(this DateTime dateTime, DateTime otherDateTime)
        {
            if (dateTime.Year == otherDateTime.Year)
            {
                return 0;
            }

            var (earlierDateTime, laterDateTime) =
                (dateTime < otherDateTime)
                ? (dateTime, otherDateTime)
                : (otherDateTime, dateTime);

            int yearDiff = laterDateTime.Year - earlierDateTime.Year;

            if (laterDateTime.Month < earlierDateTime.Month
                || (laterDateTime.Month == earlierDateTime.Month && laterDateTime.Day < earlierDateTime.Day))
            {
                --yearDiff;
            }

            return yearDiff;
        }
    }
}
