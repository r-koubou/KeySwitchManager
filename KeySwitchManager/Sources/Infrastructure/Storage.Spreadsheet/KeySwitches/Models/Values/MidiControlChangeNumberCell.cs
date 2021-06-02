using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( int ) )]
    [ValueRange( MinValue, MinValue )]
    public partial class MidiControlChangeNumberCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;
    }
}