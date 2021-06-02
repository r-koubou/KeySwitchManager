using RkHelper.Text;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class DescriptionCell
    {
        public static readonly DescriptionCell Empty = new DescriptionCell();

        public string Value { get; }

        private DescriptionCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public DescriptionCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}