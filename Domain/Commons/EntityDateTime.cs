using System;

using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Domain.Commons
{
    /// <summary>
    /// Represents a date and time. The time zone is assumed to be handled in UTC.
    /// </summary>
    public class EntityDateTime : IEquatable<EntityDateTime>
    {
        public static EntityDateTime Now
        {
            get
            {
                var now = TimeZoneInfo.ConvertTimeToUtc( DateTime.Now );

                return new EntityDateTime(
                    now.Year,
                    now.Month,
                    now.Day,
                    now.Hour,
                    now.Minute,
                    now.Second,
                    now.Millisecond
                );
            }
        }
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int MilliSecond { get; }

        public EntityDateTime( DateTime dateTime )
            : this( dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
            )
        {}

        public EntityDateTime(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            int milliSecond )
        {
            RangeValidateHelper.ValidateRange( year,        0, int.MaxValue );
            RangeValidateHelper.ValidateRange( month,       1, 12 );
            RangeValidateHelper.ValidateRange( day,         1, 31 );
            RangeValidateHelper.ValidateRange( hour,        0, 23 );
            RangeValidateHelper.ValidateRange( minute,      0, 59 );
            RangeValidateHelper.ValidateRange( second,      0, 59 );
            RangeValidateHelper.ValidateRange( milliSecond, 0, 9999 );

            Year        = year;
            Month       = month;
            Day         = day;
            Hour        = hour;
            Minute      = minute;
            Second      = second;
            MilliSecond = milliSecond;
        }

        public bool Equals( EntityDateTime? other )
        {
            if( other == null )
            {
                return false;
            }

            return other.Year == Year &&
                   other.Month == Month &&
                   other.Day == Day &&
                   other.Hour == Hour &&
                   other.Minute == Minute &&
                   other.Second == Second &&
                   other.MilliSecond == MilliSecond;
        }

        public override string ToString()
            => $"{Year:D4}-{Month:D2}-{Day:D2}-{Hour:D2}:{Minute:D2}:{Second:D2}";
    }
}