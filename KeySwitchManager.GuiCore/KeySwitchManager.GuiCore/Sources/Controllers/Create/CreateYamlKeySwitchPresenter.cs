using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create.Template;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public class CreateYamlKeySwitchPresenter : ITemplateKeySwitchCreatePresenter
    {
        private ILogView View { get; }

        public CreateYamlKeySwitchPresenter( ILogView view )
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

        public void Complete( TemplateKeySwitchCreateResponse response )
        {
            View.Append( new LogViewModel( "Complete" ) );
        }
    }
}
