using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    [ValueObject( typeof( string ) )]
    public partial class ArticulationName
    {
        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}
