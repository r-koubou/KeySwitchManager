using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Applications.Standalone.Core.Presenters
{
    public sealed class CreatePresenter : ICreatePresenter
    {
        public static readonly ICreatePresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public CreatePresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleAsync( CreateOutputData outputData, CancellationToken cancellationToken = default )
        {
            if( outputData.Result )
            {
                TextView.Append( $"Created : {outputData.Value}" );
            }
            else
            {
                TextView.Append( "Failed to create." );

                if( outputData.Error?.StackTrace != null )
                {
                    TextView.Append( outputData.Error.StackTrace );
                }
            }

            await Task.CompletedTask;
        }

        private class NullImpl : ICreatePresenter
        {
            public async Task HandleAsync( CreateOutputData outputData, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }
        }
    }
}
