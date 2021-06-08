using ValueObjectGenerator;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Models.Values
{
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class DeveloperNameCell
    {
        public static readonly DeveloperNameCell Empty = new ( "Developer Name" );
    }
}