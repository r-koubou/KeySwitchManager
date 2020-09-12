
using System;

using ArticulationManager.Domain.Commons;

namespace ArticulationManager.Domain.Services
{
    public static class EntityDateTimeService
    {
        public static EntityDateTime FromDateTime( DateTime dateTime )
        {
            return new EntityDateTime(
                dateTime.Year,
                dateTime.Month,
                dateTime.Day,
                dateTime.Hour,
                dateTime.Minute,
                dateTime.Second,
                dateTime.Millisecond
            );
        }

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