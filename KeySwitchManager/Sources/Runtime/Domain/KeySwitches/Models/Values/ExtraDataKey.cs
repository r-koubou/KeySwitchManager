using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ExtraDataKey {}
}