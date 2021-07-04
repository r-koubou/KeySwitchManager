using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class DeveloperNameCell
    {
        public static readonly DeveloperNameCell Empty = new ( "Developer Name" );
    }
}