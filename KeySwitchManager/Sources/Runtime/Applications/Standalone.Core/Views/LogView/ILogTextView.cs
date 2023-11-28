using System;

namespace KeySwitchManager.Applications.Standalone.Core.Views.LogView
{
    public interface ILogTextView
    {
        static readonly ILogTextView Null = new NullImpl();

        bool AutoScroll { get; set; }

        void Append( string text );
        void AppendError( string text );
        void AppendError( Exception exception );
        void Clear();
        void ScrollToEnd();

        private class NullImpl : ILogTextView
        {
            public bool AutoScroll { get; set; }

            public void Append( string text ) {}
            public void AppendError( string text ) {}
            public void AppendError( Exception exception ) {}
            public void Clear() {}
            public void ScrollToEnd() {}
        }
    }
}
