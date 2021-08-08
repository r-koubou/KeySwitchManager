using System;

namespace KeySwitchManager.Applications.Core.Views.LogView
{
    public class ConsoleLogView : ILogTextView
    {
        public virtual bool AutoScroll { get; set; } // Ignore

        public virtual void Append( string text )
        {
            Console.Out.WriteLine( text );
        }

        public void AppendError( string text )
        {
            Console.Error.WriteLine( text );
        }

        public virtual void AppendError( Exception exception )
        {
            Console.Error.WriteLine( exception.ToString() );
        }

        public virtual void Clear()
        {
            // None
        }

        public virtual void ScrollToEnd()
        {
            // None
        }
    }
}