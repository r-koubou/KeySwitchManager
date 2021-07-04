using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public class ExportXlsxPresenter : IExportSpreadsheetPresenter
    {
        private ILogTextView TextView { get; }

        public ExportXlsxPresenter( ILogTextView textView )
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

        public void Complete( ExportSpreadsheetResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
