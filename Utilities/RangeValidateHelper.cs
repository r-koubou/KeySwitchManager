namespace ArticulationManager.Utilities
{
    public static class RangeValidateHelper
    {
        public static void ValidateIntRange( int value, int min, int max )
        {
            if( value < min || value > max )
            {
                throw new ValueOutOfRangeException( value, min, max );
            }
        }
        public static void ValidateIntMinValue( int value, int min )
        {
            if( value < min )
            {
                throw new ValueOutOfRangeException( value );
            }
        }
        public static void ValidateIntMaxValue( int value, int max )
        {
            if( value > max )
            {
                throw new ValueOutOfRangeException( value );
            }
        }
    }
}