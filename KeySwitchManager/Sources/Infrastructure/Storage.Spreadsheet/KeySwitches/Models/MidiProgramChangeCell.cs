using RkHelper.Number;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class MidiProgramChangeCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiProgramChangeCell( int value )
        {
            NumberHelper.ValidateRange( value, MinValue, MaxValue );
            Value = value;
        }
    }
}