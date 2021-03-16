using RkHelper.Number;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
{
    public class MidiControlChangeNumberCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiControlChangeNumberCell( int ccNumber )
        {
            NumberHelper.ValidateRange( ccNumber, MinValue, MaxValue );
            Value = ccNumber;
        }
    }
}