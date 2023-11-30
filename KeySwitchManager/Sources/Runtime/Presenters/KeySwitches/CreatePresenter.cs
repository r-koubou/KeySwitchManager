using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Presenters
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

        #region NullObject
        private class NullImpl : ICreatePresenter
        {
            public async Task HandleAsync( CreateOutputData outputData, CancellationToken cancellationToken = default )
                => await Task.CompletedTask;
        }
        #endregion
    }
}
