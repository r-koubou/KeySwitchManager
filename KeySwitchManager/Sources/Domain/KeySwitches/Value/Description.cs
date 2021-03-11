using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A description of keyswitch
    /// </summary>
    [ValueObject( typeof( string ) )]
    public partial class Description
    {
        private static partial string Validate( string value )
            => StringHelper.IsEmpty( value ) ? string.Empty : value;
    }
}