using System;

using AppKit;

using Foundation;

using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Xamarin.Mac.UiKitView
{
    public class LogTextView : ILogTextView
    {
        private NSTextView Target { get; }
        private ViewController UiThreadDispatcher { get; }

        #region ILogTextView
        public bool AutoScroll { get; set; } = true;

        public LogTextView( NSTextView target, ViewController uiThreadDispatcher )
        {
            Target             = target;
            UiThreadDispatcher = uiThreadDispatcher;
        }

        public void Append( string text )
        {
            UiThreadDispatcher.InvokeOnMainThread( () =>
            {
                Target.Value += text + Environment.NewLine;

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
            UiThreadDispatcher.InvokeOnMainThread( () =>
            {
                Target.Value = string.Empty;
            });
        }

        public void ScrollToEnd()
        {
            UiThreadDispatcher.InvokeOnMainThread( () => {
                var range = new NSRange( Target.Value.Length, 0 );
                Target.ScrollRangeToVisible( range );
            });
        }
        #endregion
    }
}
