namespace ArticulationManager.Domain.Commons
{
    public interface IText
    {
        public string Text { get; }

        public class PlainText : IText
        {
            public string Text { get; }

            public PlainText( string text )
            {
                Text = text;
            }

            public override string ToString() => Text;
        }
    }
}