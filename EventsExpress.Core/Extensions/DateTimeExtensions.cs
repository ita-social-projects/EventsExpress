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
            int yearDiff = dateTime.Year - otherDateTime.Year;

            if (dateTime.Month < otherDateTime.Month
                || (dateTime.Month == otherDateTime.Month && dateTime.Day < otherDateTime.Day))
            {
                --yearDiff;
            }

            return yearDiff;
        }
    }
}
