using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    [ValueObject( typeof( string ) )]
    public partial class ExtraDataKey
    {
        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}