using System.Text;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Delete;
using KeySwitchManager.UseCase.KeySwitches.Find;

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

        public void Complete( FindResponse response )
        {
            var sb = new StringBuilder( 128 );

            foreach( var x in response.Result )
            {
                sb.Clear();
                sb.Append( x.DeveloperName ).Append( ", " )
                  .Append( x.ProductName ).Append( ", " )
                  .Append( x.InstrumentName );

                TextView.Append( sb.ToString() );
            }

            TextView.Append( "---------------------------------" );
            TextView.Append( $"{response.FoundCount} record(s) deleted" );
            TextView.Append( "Complete" );

            TextView.ScrollToEnd();
        }
    }
}
