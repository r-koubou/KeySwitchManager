using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class InstrumentName
    {
        public static readonly InstrumentName Any = new InstrumentName( "*" );
    }
}