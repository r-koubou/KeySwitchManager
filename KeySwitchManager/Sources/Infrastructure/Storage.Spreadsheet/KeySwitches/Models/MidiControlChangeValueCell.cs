using RkHelper.Number;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class MidiControlChangeValueCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiControlChangeValueCell( int ccValue )
        {
            NumberHelper.ValidateRange( ccValue, MinValue, MaxValue );
            Value = ccValue;
        }
    }
}