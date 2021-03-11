using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ArticulationName {}
}
