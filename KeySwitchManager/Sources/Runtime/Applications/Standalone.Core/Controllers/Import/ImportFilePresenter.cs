using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Import
{
    public class ImportFilePresenter : IImportFilePresenter
    {
        private ILogTextView TextView { get; }

        public ImportFilePresenter( ILogTextView textView )
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

        public void Complete( ImportFileResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
