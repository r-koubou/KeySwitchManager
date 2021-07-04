using KeySwitchManager.Core.Applications.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet;

namespace KeySwitchManager.Core.Applications.Controllers.Create
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
