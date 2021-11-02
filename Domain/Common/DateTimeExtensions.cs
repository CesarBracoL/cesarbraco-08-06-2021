using System;

namespace Domain.Common
{
    public static class DateTimeExtensions
    {
        public static string ToStringOrEmptyWithFormat(this DateTime? datetime, string format = null)
        {
            if (datetime == null)
                return null;

            var dateTimeUtcOffset = new DateTimeOffset(
                datetime.Value.Year,
                datetime.Value.Month,
                datetime.Value.Day,
                datetime.Value.Hour,
                datetime.Value.Minute,
                datetime.Value.Second,
                datetime.Value.Millisecond,
                new TimeSpan(Constants.UtcLocal, 0, 0));

            return dateTimeUtcOffset.ToString(format);
        }

        public static string ToStringOrEmptyWithFormat(this DateTime datetime, string format = null)
        {
            var dateTimeUtcOffset = new DateTimeOffset(
                datetime.Year,
                datetime.Month,
                datetime.Day,
                datetime.Hour,
                datetime.Minute,
                datetime.Second,
                datetime.Millisecond,
                new TimeSpan(Constants.UtcLocal, 0, 0));

            return dateTimeUtcOffset.ToString(format);
        }

        public static string ToStringWithFormatUtc(this DateTime? datetime)
        {
            if (datetime == null)
                return null;

            var dateTimeUtcOffset = new DateTimeOffset(
                datetime.Value.Year,
                datetime.Value.Month,
                datetime.Value.Day,
                datetime.Value.Hour,
                datetime.Value.Minute,
                datetime.Value.Second,
                datetime.Value.Millisecond,
                new TimeSpan(Constants.UtcLocal, 0, 0));

            return dateTimeUtcOffset.ToString(Constants.DateTimeFormatUtc);
        }

        public static string ToStringWithFormatUtc(this DateTime datetime)
        {
            var dateTimeUtcOffset = new DateTimeOffset(
                datetime.Year,
                datetime.Month,
                datetime.Day,
                datetime.Hour,
                datetime.Minute,
                datetime.Second,
                datetime.Millisecond,
                new TimeSpan(Constants.UtcLocal, 0, 0));

            return dateTimeUtcOffset.ToString(Constants.DateTimeFormatUtc);
        }
    }
}
