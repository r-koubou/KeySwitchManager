using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class InstrumentName {}
}