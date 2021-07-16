using ValueObjectGenerator;

namespace KeySwitchManager.Commons.Data
{
    [ValueObject( typeof( string ), Option = ValueOption.NonValidating )]
    public partial class PlainText : IText
    {
        public static IText Empty { get; } = new PlainText( "" );
    }
}