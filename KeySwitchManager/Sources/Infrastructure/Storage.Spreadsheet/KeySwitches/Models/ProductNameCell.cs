using RkHelper.Text;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class ProductNameCell
    {
        public static readonly ProductNameCell Empty = new ProductNameCell();

        public string Value { get; }

        private ProductNameCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public ProductNameCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}