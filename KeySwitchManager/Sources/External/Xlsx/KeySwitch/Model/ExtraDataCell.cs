using RkHelper.Text;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
{
    public class ExtraDataCell
    {
        public static readonly ExtraDataCell Empty
            = new ExtraDataCell( CellConstants.NotAvailableCellValue );

        public string Value { get; }

        public ExtraDataCell( string name )
        {
            Value = StringHelper.IsEmpty( name ) ? string.Empty : name;
        }
    }
}