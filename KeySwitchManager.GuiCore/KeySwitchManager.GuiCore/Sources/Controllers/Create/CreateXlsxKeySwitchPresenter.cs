using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate;
using KeySwitchManager.UseCase.KeySwitches.Create.Template;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public class CreateXlsxKeySwitchPresenter : ISpreadsheetTemplateExportPresenter
    {
        private ILogView View { get; }

        public CreateXlsxKeySwitchPresenter( ILogView view )
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

        public void Complete( SpreadsheetTemplateExportResponse response )
        {
            View.Append( new LogViewModel( "Complete" ) );
        }
    }
}
