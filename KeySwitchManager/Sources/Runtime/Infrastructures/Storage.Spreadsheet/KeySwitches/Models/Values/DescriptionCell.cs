using RkHelper.Text;

using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    public partial class DescriptionCell
    {
        public static readonly DescriptionCell Empty = new(string.Empty);

        private static partial string Validate( string value )
            => StringHelper.IsEmpty( value ) ? string.Empty : value;
    }
}