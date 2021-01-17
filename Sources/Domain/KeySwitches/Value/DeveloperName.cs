using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    [ValueObject(typeof(string))]
    public partial class DeveloperName
    {
        private static partial string Validate( string value )
        {
            StringHelper.ValidateEmpty( value );
            return value;
        }
    }
}
