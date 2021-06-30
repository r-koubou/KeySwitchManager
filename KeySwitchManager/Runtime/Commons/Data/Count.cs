using System;

using ValueObjectGenerator;

namespace KeySwitchManager.Commons.Data
{
    [ValueObject(typeof(int))]
    public partial class Count
    {
        private static partial int Validate( int value )
        {
            if( value < 0 )
            {
                throw new ArgumentException( $"value must be > 0 (={value})" );
            }
            return value;
        }
    }
}
