using KeySwitchManager.GuiCore.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.GuiCore.Controllers.Import
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
