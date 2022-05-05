using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Core.Controllers.Delete
{
    public class DeletePresenter : IDeletePresenter
    {
        private ILogTextView TextView { get; }

        public DeletePresenter( ILogTextView textView )
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

        public void Complete( DeleteResponse response )
        {
            TextView.Append( "---------------------------------" );
            TextView.Append( $"{response.RemovedCount} record(s) deleted" );
            TextView.Append( "Complete" );

            TextView.ScrollToEnd();
        }
    }
}
