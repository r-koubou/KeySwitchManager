using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class InstrumentNameCell
    {
        public static readonly InstrumentNameCell Empty = new("Instrument Name");
    }
}