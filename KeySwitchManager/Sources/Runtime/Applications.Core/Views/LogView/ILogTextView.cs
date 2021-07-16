namespace KeySwitchManager.Applications.Core.Views.LogView
{
    public interface ILogTextView
    {
        public bool AutoScroll { get; set; }

        void Append( string text );
        void AppendError( string text );
        void AppendError( System.Exception exception );
        void Clear();
        void ScrollToEnd();

        public class Null : ILogTextView
        {
            public bool AutoScroll { get; set; }

            public void Append( string text )
            {}

            public void AppendError( string text )
            {}

            public void AppendError( System.Exception exception )
            {}

            public void Clear()
            {}

            public void ScrollToEnd()
            {}
        }
    }
}
