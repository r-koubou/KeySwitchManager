using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    [ValueObject( typeof( string ) )]
    [NotEmpty]
    public partial class DeveloperName
    {
        public static readonly DeveloperName Any = new DeveloperName( "*" );
    }
}
