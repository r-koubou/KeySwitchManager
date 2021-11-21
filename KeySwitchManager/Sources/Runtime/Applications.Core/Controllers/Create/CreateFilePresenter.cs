using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateFilePresenter : ICreateFilePresenter
    {
        private ILogTextView TextView { get; }

        public CreateFilePresenter( ILogTextView textView )
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

        public void Complete( CreateFileResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
