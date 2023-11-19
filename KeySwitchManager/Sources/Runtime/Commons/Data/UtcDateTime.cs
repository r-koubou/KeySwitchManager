using System;

using RkHelper.Primitives;

namespace KeySwitchManager.Commons.Data
{
    /// <summary>
    /// Represents a date and time. The time zone is assumed to be handled in UTC.
    /// </summary>
    public class UtcDateTime : IEquatable<UtcDateTime>
    {
        public static UtcDateTime Now
        {
            get
            {
                var now = NowAsDateTime;

                return new UtcDateTime(
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

        public static DateTime NowAsDateTime
            => DateTime.UtcNow;

        public int Year { get; }
        public int Month { get; }
        public int Day { get; }
        public int Hour { get; }
        public int Minute { get; }
        public int Second { get; }
        public int MilliSecond { get; }

        public UtcDateTime( DateTime dateTime )
            : this( dateTime.Year,
                    dateTime.Month,
                    dateTime.Day,
                    dateTime.Hour,
                    dateTime.Minute,
                    dateTime.Second,
                    dateTime.Millisecond
            )
        {}

        public UtcDateTime(
            int year,
            int month,
            int day,
            int hour,
            int minute,
            int second,
            int milliSecond )
        {
            NumberHelper.ValidateRange( year,        0, int.MaxValue );
            NumberHelper.ValidateRange( month,       1, 12 );
            NumberHelper.ValidateRange( day,         1, 31 );
            NumberHelper.ValidateRange( hour,        0, 23 );
            NumberHelper.ValidateRange( minute,      0, 59 );
            NumberHelper.ValidateRange( second,      0, 59 );
            NumberHelper.ValidateRange( milliSecond, 0, 9999 );

            Year        = year;
            Month       = month;
            Day         = day;
            Hour        = hour;
            Minute      = minute;
            Second      = second;
            MilliSecond = milliSecond;
        }

        public override string ToString()
            => $"{Year:D4}-{Month:D2}-{Day:D2}-{Hour:D2}:{Minute:D2}:{Second:D2}";

        #region Equality
        public override int GetHashCode() =>
            HashCode.Combine(
                Year,
                Month,
                Day,
                Hour,
                Minute,
                Second,
                MilliSecond
            );

        public bool Equals( UtcDateTime? other )
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

        public override bool Equals(object? obj)
        {
            return Equals(obj as UtcDateTime);
        }
        #endregion

    }
}
