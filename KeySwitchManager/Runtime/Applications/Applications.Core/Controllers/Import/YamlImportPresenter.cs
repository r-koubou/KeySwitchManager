using Application.Core.Views.LogView;

using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace Application.Core.Controllers.Import
{
    public class YamlImportPresenter : IImportTextPresenter
    {
        private ILogTextView TextView { get; }

        public YamlImportPresenter( ILogTextView textView )
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

        public void Complete( ImportTextResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
