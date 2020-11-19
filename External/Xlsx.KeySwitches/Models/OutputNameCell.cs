using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
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
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }

            Value = name;
        }
    }
}