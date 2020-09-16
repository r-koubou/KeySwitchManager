namespace KeySwitchManager.Domain.Commons
{
    public class PlainText : IText
    {
        public string Value { get; }

        public PlainText( string text )
        {
            Value = text;
        }

        public override string ToString() => Value;
    }
}