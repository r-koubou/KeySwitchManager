using RkHelper.Text;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models
{
    public class AuthorCell
    {
        public static readonly AuthorCell Empty = new AuthorCell();

        public string Value { get; }

        private AuthorCell()
        {
            Value = CellConstants.EmptyCellValue;
        }

        public AuthorCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}