using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A ProductName name
    /// </summary>
    [ValueObject( typeof( string ) )]
    public partial class ProductName
    {
        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}
