using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ArticulationNameCell
    {
        public static readonly ArticulationNameCell Empty = new ( CellConstants.NotAvailableCellValue );
    }
}