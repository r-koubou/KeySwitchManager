using RkHelper.Number;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class MidiChannelCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;

        public int Value { get; }

        public MidiChannelCell( int value )
        {
            NumberHelper.ValidateRange( value, MinValue, MaxValue );
            Value = value;
        }
    }
}