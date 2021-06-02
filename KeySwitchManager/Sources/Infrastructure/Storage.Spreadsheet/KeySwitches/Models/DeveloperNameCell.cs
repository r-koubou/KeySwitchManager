using RkHelper.Text;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class DeveloperNameCell
    {
        public static readonly DeveloperNameCell Empty = new DeveloperNameCell();

        public string Value { get; }

        private DeveloperNameCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public DeveloperNameCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}