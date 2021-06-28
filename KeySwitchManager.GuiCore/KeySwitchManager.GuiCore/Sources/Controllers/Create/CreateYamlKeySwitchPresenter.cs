using KeySwitchManager.GuiCore.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create.Text;

namespace KeySwitchManager.GuiCore.Controllers.Create
{
    public class CreateYamlKeySwitchPresenter : ICreateTextTemplatePresenter
    {
        private ILogTextView TextView { get; }

        public CreateYamlKeySwitchPresenter( ILogTextView textView )
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

        public void Complete( CreateTextTemplateResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
