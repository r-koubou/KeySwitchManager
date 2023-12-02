using System;
using System.Windows.Controls;
using System.Windows.Threading;

using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.WPF.WpfView
{
    public class LogTextView : ILogTextView
    {
        private TextBox Target { get; }
        private Dispatcher UiThreadDispatcher { get; }

        #region ILogTextView
        public bool AutoScroll { get; set; } = true;

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

                if( AutoScroll )
                {
                    ScrollToEnd();
                }
            });
        }

        public void AppendError( string text )
        {
            Append( text );
        }

        public void AppendError( Exception exception )
        {
            AppendError( exception.ToString() );
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
        #endregion
    }
}
