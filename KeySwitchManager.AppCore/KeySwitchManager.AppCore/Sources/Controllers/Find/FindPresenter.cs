using System.Text;

using KeySwitchManager.AppCore.View.LogView;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.AppCore.Controllers.Find
{
    public class FindPresenter : IFindPresenter
    {
        private ILogTextView TextView { get; }

        public FindPresenter( ILogTextView textView )
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
            var i = 1;

            foreach( var x in response.Result )
            {
                sb.Clear();
                sb.Append( $"[{i:D5}] " )
                  .Append( x.DeveloperName ).Append( ", " )
                  .Append( x.ProductName ).Append( ", " )
                  .Append( x.InstrumentName );

                TextView.Append( sb.ToString() );
                i++;
            }
            TextView.Append( "---------------------------------" );
            TextView.Append( $"{response.FoundCount} record(s) found" );
            TextView.Append( "Complete" );

            TextView.ScrollToEnd();
        }
    }
}
