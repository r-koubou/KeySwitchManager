using System;

namespace KeySwitchManager.Applications.Standalone.Core.Views.LogView
{
    public interface ILogTextView
    {
        public bool AutoScroll { get; set; }

        void Append( string text );
        void AppendError( string text );
        void AppendError( Exception exception );
        void Clear();
        void ScrollToEnd();

        public class Null : ILogTextView
        {
            public bool AutoScroll { get; set; }

            public void Append( string text )
            {}

            public void AppendError( string text )
            {}

            public void AppendError( Exception exception )
            {}

            public void Clear()
            {}

            public void ScrollToEnd()
            {}
        }
    }
}
