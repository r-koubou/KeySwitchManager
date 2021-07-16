using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public class ExportDawPresenter : IExportDawPresenter
    {
        private ILogTextView TextView { get; }

        public ExportDawPresenter( ILogTextView textView )
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

        public void Complete( ExportDawResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
