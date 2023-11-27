using System.Threading;
using System.Threading.Tasks;

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

        public async Task HandleAsync( CreateResponse response, CancellationToken cancellationToken = default )
        {
            if( response.Result )
            {
                TextView.Append( $"Created : {response.Response}" );
            }
            else
            {
                TextView.Append( "Failed to create." );

                if( response.Error?.StackTrace != null )
                {
                    TextView.Append( response.Error.StackTrace );
                }
            }

            await Task.CompletedTask;
        }
    }
}
