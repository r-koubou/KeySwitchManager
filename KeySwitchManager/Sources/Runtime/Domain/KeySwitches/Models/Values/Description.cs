using RkHelper.Primitives;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
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
