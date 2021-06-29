namespace KeySwitchManager.AppCore.Views.LogView
{
    public interface ILogTextView
    {
        public bool AutoScroll { get; set; }

        void Append( string text );
        void Clear();
        void ScrollToEnd();

        public class Null : ILogTextView
        {
            public bool AutoScroll { get; set; }

            public void Append( string text )
            {}

            public void Clear()
            {}

            public void ScrollToEnd()
            {}
        }
    }
}
