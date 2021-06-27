using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public class ImportSpreadSheetPresenter : IImportSpreadsheetPresenter
    {
        private ILogView View { get; }

        public ImportSpreadSheetPresenter( ILogView view )
        {
            View = view;
        }

        public void Present<T>( T param )
        {
            if( param != null )
            {
                View.Append( new LogViewModel( param.ToString() ?? string.Empty ) );
            }
        }

        public void Complete( ImportSpreadSheetResponse response )
        {
            View.Append( new LogViewModel( "Complete" ) );
        }
    }
}
