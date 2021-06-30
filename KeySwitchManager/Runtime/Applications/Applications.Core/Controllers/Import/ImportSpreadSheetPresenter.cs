using Application.Core.Views.LogView;

using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace Application.Core.Controllers.Import
{
    public class ImportSpreadSheetPresenter : IImportSpreadsheetPresenter
    {
        private ILogTextView TextView { get; }

        public ImportSpreadSheetPresenter( ILogTextView textView )
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

        public void Complete( ImportSpreadSheetResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
