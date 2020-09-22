using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class ColorIndexCell
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public static readonly ColorIndexCell Default = new ColorIndexCell( MinValue );

        public int Value { get; }

        public ColorIndexCell( int index )
        {
            RangeValidateHelper.ValidateRange( index, MinValue, MaxValue );
            Value = index;
        }
    }
}