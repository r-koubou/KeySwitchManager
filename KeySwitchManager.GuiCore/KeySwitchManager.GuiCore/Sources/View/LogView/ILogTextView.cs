namespace KeySwitchManager.GuiCore.View.LogView
{
    public interface ILogTextView
    {
        void Append( string text );
        void Clear();
        void ScrollToEnd();

        public class Null : ILogTextView
        {
            public void Append( string text )
            {}

            public void Clear()
            {}

            public void ScrollToEnd()
            {}
        }
    }
}
