using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ExtraDataKey {}
}