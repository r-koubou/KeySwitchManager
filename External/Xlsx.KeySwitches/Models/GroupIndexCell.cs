using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class GroupIndexCell
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public static readonly GroupIndexCell Default = new GroupIndexCell( MinValue );

        public int Value { get; }

        public GroupIndexCell( int index )
        {
            RangeValidateHelper.ValidateRange( index, MinValue, MaxValue );
            Value = index;
        }
    }
}