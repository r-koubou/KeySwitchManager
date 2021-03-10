using ValueObjectGenerator;

namespace KeySwitchManager.Domain.Commons
{
    [ValueObject( typeof( string ), Option = ValueOption.NonValidating )]
    public partial class PlainText : IText
    {
        public static IText Empty { get; } = new PlainText( "" );
    }
}