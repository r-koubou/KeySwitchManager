using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class InstrumentName {}
}