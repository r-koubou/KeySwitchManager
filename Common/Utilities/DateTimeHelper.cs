using System;

namespace ArticulationManager.Common.Utilities
{
    public static class DateTimeHelper
    {
        public static DateTime NowUtc() => TimeZoneInfo.ConvertTimeToUtc( DateTime.Now );
        public static DateTime ToUtc( DateTime dateTime ) => TimeZoneInfo.ConvertTimeToUtc( dateTime );
        public static DateTime ToLocalTime( DateTime localDateTime ) => TimeZoneInfo.ConvertTimeFromUtc( localDateTime, TimeZoneInfo.Local );
    }
}