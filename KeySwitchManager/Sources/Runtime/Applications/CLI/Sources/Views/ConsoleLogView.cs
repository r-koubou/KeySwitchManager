using System;

using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.CLI.Views
{
    public sealed class ConsoleLogView : ILogTextView
    {
        public bool AutoScroll { get; set; } // Ignore

        public void Append( string text )
        {
            Console.Out.WriteLine( text );
        }

        public void AppendError( string text )
        {
            Console.Error.WriteLine( text );
        }

        public void AppendError( Exception exception )
        {
            Console.Error.WriteLine( exception.ToString() );
        }

        public void Clear()
        {
            // None
        }

        public void ScrollToEnd()
        {
            // None
        }
    }
}
