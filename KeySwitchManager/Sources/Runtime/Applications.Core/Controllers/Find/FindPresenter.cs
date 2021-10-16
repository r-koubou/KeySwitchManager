using System.Linq;
using System.Text;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.Applications.Core.Controllers.Find
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

            foreach( var k in response.Result.OrderBy( x => x.DeveloperName.Value )
                                      .ThenBy( x=> x.ProductName.Value)
                                      .ThenBy( x => x.InstrumentName.Value ) )
            {
                sb.Clear();
                sb.Append( k );
                TextView.Append( sb.ToString() );
            }

            TextView.Append( "---------------------------------" );
            TextView.Append( $"{response.FoundCount} record(s) found" );
            TextView.Append( "Complete" );

            TextView.ScrollToEnd();
        }
    }
}
