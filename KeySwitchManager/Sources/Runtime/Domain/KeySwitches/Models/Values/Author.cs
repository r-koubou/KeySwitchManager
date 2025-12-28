using RkHelper.Primitives;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    /// <summary>
    /// A created author name
    /// </summary>
    [ValueObject(typeof(string))]
    public partial class Author
    {
        private static partial string Validate( string value )
            => StringHelper.IsEmpty( value ) ? string.Empty : value;
    }
}
