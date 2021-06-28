using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public class CreateXlsxKeySwitchPresenter : ICreateSpreadsheetTemplatePresenter
    {
        private ILogTextView TextView { get; }

        public CreateXlsxKeySwitchPresenter( ILogTextView textView )
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

        public void Complete( CreateSpreadsheetTemplateResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
