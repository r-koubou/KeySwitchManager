using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class MidiControlChangeNumberCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiControlChangeNumberCell( int ccNumber )
        {
            RangeValidateHelper.ValidateRange( ccNumber, MinValue, MaxValue );
            Value = ccNumber;
        }
    }
}