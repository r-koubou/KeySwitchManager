using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A ProductName name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ProductName {}
}
