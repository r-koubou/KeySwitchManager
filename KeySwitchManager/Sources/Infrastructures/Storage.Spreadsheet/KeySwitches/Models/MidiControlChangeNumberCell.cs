using RkHelper.Number;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models
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