using System;

namespace KeySwitchManager.Domain.Commons.Helpers
{
    public static class UtcDateTimeHelper
    {
        public static DateTime ToDateTime( UtcDateTime dateTime )
        {
            return new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: dateTime.Hour,
                minute: dateTime.Minute,
                second: dateTime.Second,
                millisecond: dateTime.MilliSecond,
                DateTimeKind.Utc
            );
        }

        public static DateTime ToLocalDateTime( UtcDateTime dateTime )
        {
            return new DateTime(
                year: dateTime.Year,
                month: dateTime.Month,
                day: dateTime.Day,
                hour: dateTime.Hour,
                minute: dateTime.Minute,
                second: dateTime.Second,
                millisecond: dateTime.MilliSecond,
                DateTimeKind.Local
            );
        }

    }
}