using System;

namespace KeySwitchManager.Commons.Data.Extensions
{
    public static class UtcDateTimeExtension
    {
        public static DateTime As( this UtcDateTime utcDateTime )
        {
            return new DateTime(
                kind: DateTimeKind.Utc,
                year: utcDateTime.Year,
                month: utcDateTime.Month,
                day: utcDateTime.Day,
                hour: utcDateTime.Hour,
                minute: utcDateTime.Minute,
                second: utcDateTime.Second,
                millisecond: utcDateTime.MilliSecond
            );
        }
    }
}
