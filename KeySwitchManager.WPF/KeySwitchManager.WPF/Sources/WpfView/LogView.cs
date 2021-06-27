using System;
using System.Windows.Controls;
using System.Windows.Threading;

using KeySwitchManager.GuiCore.Sources.View.LogView;

namespace KeySwitchManager.WPF.WpfView
{
    public class LogView : ILogView
    {
        private TextBox Target { get; }
        private Dispatcher UiThreadDispatcher { get; }

        public LogView( TextBox target, Dispatcher uiThreadDispatcher )
        {
            Target                   = target;
            UiThreadDispatcher = uiThreadDispatcher;
        }

        public void Append( LogViewModel model )
        {
            UiThreadDispatcher.Invoke( () =>
            {
                Target.Text += model.Text + Environment.NewLine;
            });
        }

        public void Clear()
        {
            UiThreadDispatcher.Invoke( () =>
            {
                Target.Text = string.Empty;
            });
        }
    }
}
