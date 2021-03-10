using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ExtraDataKey {}
}