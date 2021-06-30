using Application.Core.Views.LogView;

using KeySwitchManager.UseCase.KeySwitches.Create.Text;

namespace Application.Core.Controllers.Create
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
