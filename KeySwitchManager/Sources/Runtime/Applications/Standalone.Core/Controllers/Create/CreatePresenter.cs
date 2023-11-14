using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Create
{
    public class CreatePresenter : ICreatePresenter
    {
        private ILogTextView TextView { get; }

        public CreatePresenter( ILogTextView textView )
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

        public void Complete( CreateResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
