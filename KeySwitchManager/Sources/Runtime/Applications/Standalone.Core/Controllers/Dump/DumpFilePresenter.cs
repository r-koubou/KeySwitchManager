using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Dump;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Dump
{
    public class DumpFilePresenter : IDumpFilePresenter
    {
        private ILogTextView TextView { get; }

        public DumpFilePresenter( ILogTextView textView )
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

        public void Complete( DumpFileResponse response )
        {
            TextView.Append( "Complete" );
        }
    }
}
