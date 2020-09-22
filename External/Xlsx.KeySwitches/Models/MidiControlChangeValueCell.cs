using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class MidiControlChangeValueCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiControlChangeValueCell( int ccValue )
        {
            RangeValidateHelper.ValidateRange( ccValue, MinValue, MaxValue );
            Value = ccValue;
        }
    }
}