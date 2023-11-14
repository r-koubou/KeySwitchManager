using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Export
{
    public class ExportFilePresenter : IExportFilePresenter
    {
        private ILogTextView TextView { get; }

        public ExportFilePresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public void Present<T>( T param )
        {
            if( param != null )
            {
                TextView.Append( param.ToString() ?? string.Empty );
            }
        }

        public void Complete( ExportFileResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
