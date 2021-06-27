﻿using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public class SpreadsheetImportPresenter : ISpreadsheetImportPresenter
    {
        private ILogView View { get; }

        public SpreadsheetImportPresenter( ILogView view )
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

        public void Complete( SpreadsheetImportResponse response )
        {
            View.Append( new LogViewModel( "Complete" ) );
        }
    }
}
