using System;

namespace KeySwitchManager.Domain.Commons.Helper
{
    public static class EntityDateTimeHelper
    {
        public static DateTime ToDateTime( EntityDateTime dateTime )
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

        public static DateTime ToLocalDateTime( EntityDateTime dateTime )
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