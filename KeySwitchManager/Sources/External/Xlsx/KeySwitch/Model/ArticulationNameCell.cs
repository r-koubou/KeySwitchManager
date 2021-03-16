using RkHelper.Text;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
{
    public class ArticulationNameCell
    {
        public static readonly ArticulationNameCell Empty
            = new ArticulationNameCell( CellConstants.NotAvailableCellValue );

        public string Value { get; }

        public ArticulationNameCell( string name )
        {
            StringHelper.ValidateEmpty( name );
            Value = name;
        }
    }
}