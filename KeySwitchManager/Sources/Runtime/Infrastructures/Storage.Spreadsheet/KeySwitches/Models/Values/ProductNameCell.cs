using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ProductNameCell
    {
        public static readonly ProductNameCell Empty = new("Product Name");
    }
}