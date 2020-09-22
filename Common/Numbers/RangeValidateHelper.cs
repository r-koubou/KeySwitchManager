using System;

using KeySwitchManager.Common.Exceptions;

namespace KeySwitchManager.Common.Numbers
{
    public static class RangeValidateHelper
    {
        public static void ValidateRange<T>( IComparable<T> value, T min, T max ) where T : notnull
        {
            if( value.CompareTo( min ) < 0 )
            {
                throw new ValueOutOfRangeException( value, min, max );
            }

            if( value.CompareTo( max ) > 0 )
            {
                throw new ValueOutOfRangeException( value, min, max );
            }
        }
    }
}