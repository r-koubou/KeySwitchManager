using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ArticulationName {}
}
