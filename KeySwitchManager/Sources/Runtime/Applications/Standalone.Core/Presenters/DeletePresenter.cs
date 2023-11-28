using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Standalone.Core.Presenters
{
    public sealed class DeletePresenter : IDeletePresenter
    {
        public static readonly IDeletePresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public DeletePresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleDeleteBeginAsync( DeleteInputData inputData, CancellationToken cancellationToken = default )
        {
            var developerName = inputData.Value.DeveloperName;
            var productName = inputData.Value.ProductName;
            var instrumentName = inputData.Value.InstrumentName;

            TextView.Append( $"Removing keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            await Task.CompletedTask;
        }

        public async Task HandleAsync( DeleteOutputData outputData, CancellationToken cancellationToken = default )
        {
            var removedCount = outputData.Value.RemovedCount;

            if( removedCount > 0 )
            {
                TextView.Append( $"{removedCount} record(s) removed" );
            }
            else
            {
                TextView.Append( "record not found" );
            }

            await Task.CompletedTask;
        }

        #region NullObject
        private class NullImpl : IDeletePresenter
        {
            public async Task HandleDeleteBeginAsync( DeleteInputData inputData, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }

            public async Task HandleAsync( DeleteOutputData outputData, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }
        }
        #endregion
    }
}
