namespace KeySwitchManager.Domain.Commons
{
    public class PlainText : IText
    {
        public static readonly IText Empty = new PlainText( "" );

        public string Value { get; }

        public PlainText( string text )
        {
            Value = text;
        }

        public override string ToString() => Value;
    }
}