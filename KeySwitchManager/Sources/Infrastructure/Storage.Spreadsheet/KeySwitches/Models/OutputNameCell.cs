using RkHelper.Text;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class OutputNameCell
    {
        public static readonly OutputNameCell Empty = new OutputNameCell();

        public string Value { get; }

        private OutputNameCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public OutputNameCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}