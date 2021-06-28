using System;

namespace KeySwitchManager.GuiCore.View.LogView
{
    public class LogViewModel
    {
        public string Text { get; set; }

        public LogViewModel() : this( string.Empty )
        {}

        public LogViewModel( string text )
        {
            Text = text ?? throw new ArgumentNullException( $"{nameof(text)} is null" );
        }
    }
}
