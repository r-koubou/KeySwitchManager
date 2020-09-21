using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class ArticulationNameCell
    {
        public static readonly ArticulationNameCell Empty
            = new ArticulationNameCell( CellConstants.NotAvailableCellValue );

        public string Value { get; }

        public ArticulationNameCell( string name )
        {
            if( StringHelper.IsNullOrTrimEmpty( name ) )
            {
                throw new InvalidNameException( nameof( name ) );
            }

            Value = name;
        }
    }
}