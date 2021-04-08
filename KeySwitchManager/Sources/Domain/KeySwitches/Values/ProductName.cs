using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    /// <summary>
    /// A ProductName name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ProductName {}
}
