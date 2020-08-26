using System;

namespace ArticulationManager.Utilities
{
    public sealed class ValueOutOfRangeException : Exception
    {
        public ValueOutOfRangeException( int value, int minValue, int maxValue )
            : base( $"Value is {value}. Min={minValue}, Max={maxValue}" )
        {
        }

        public ValueOutOfRangeException( int value )
            : base( $"Value is {value}." )
        {
        }

    }
}