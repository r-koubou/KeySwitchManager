using System;

using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.Domain.Services
{
    public static class EntityDateTimeService
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