using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class ExtraDataCell
    {
        public static readonly ExtraDataCell Empty
            = new ExtraDataCell( CellConstants.NotAvailableCellValue );

        public string Value { get; }

        public ExtraDataCell( string name )
        {
            Value = StringHelper.IsNullOrTrimEmpty( name ) ? string.Empty : name;
        }
    }
}