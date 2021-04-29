using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ArticulationName {}
}
