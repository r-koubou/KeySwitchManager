using RkHelper.Primitives;

using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    public partial class AuthorCell
    {
        public static readonly AuthorCell Empty = new ( string.Empty );

        private static partial string Validate( string value )
            => StringHelper.IsEmpty( value ) ? string.Empty : value;
    }
}
