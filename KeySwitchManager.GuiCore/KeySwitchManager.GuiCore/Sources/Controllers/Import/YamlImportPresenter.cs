using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public class YamlImportPresenter : IImportTextPresenter
    {
        private ILogView View { get; }

        public YamlImportPresenter( ILogView view )
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

        public void Complete( ImportTextResponse response )
        {
            View.Append( new LogViewModel( "Complete" ) );
        }
    }
}
