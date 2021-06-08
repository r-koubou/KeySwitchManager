using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class ArticulationNameCell
    {
        public static readonly ArticulationNameCell Empty = new ( CellConstants.NotAvailableCellValue );
    }
}