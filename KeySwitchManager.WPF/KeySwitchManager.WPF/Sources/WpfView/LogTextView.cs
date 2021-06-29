using System;
using System.Windows.Controls;
using System.Windows.Threading;

using KeySwitchManager.AppCore.View.LogView;

namespace KeySwitchManager.WPF.WpfView
{
    public class LogTextView : ILogTextView
    {
        private TextBox Target { get; }
        private Dispatcher UiThreadDispatcher { get; }

        public LogTextView( TextBox target, Dispatcher uiThreadDispatcher )
        {
            Target             = target;
            UiThreadDispatcher = uiThreadDispatcher;
        }

        public void Append( string text )
        {
            UiThreadDispatcher.Invoke( () =>
            {
                Target.AppendText( text + Environment.NewLine );
            });
        }

        public void Clear()
        {
            UiThreadDispatcher.Invoke( () =>
            {
                Target.Text = string.Empty;
            });
        }

        public void ScrollToEnd()
        {
            UiThreadDispatcher.Invoke( () => {
                Target.Focus();
                Target.ScrollToEnd();
            });
        }
    }
}
