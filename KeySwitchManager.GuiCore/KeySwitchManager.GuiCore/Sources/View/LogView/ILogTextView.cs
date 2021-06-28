namespace KeySwitchManager.GuiCore.Sources.View.LogView
{
    public interface ILogTextView
    {
        void Append( string text );
        void Clear();

        public class Null : ILogTextView
        {
            public void Append( string text )
            {}

            public void Clear()
            {}
        }
    }
}
