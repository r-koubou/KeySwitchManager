using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.UseCase.KeySwitches.Delete;
using KeySwitchManager.UseCase.KeySwitches.Dump;

namespace KeySwitchManager.Applications.Standalone.Core.Presenters
{
    public sealed class DumpPresenter : IDumpPresenter
    {
        public static readonly IDeletePresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public DumpPresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleAsync( DumpOutputData outputData, CancellationToken cancellationToken = default )
        {
            TextView.Append( $"{outputData.Value.DumpDataCount} record(s) dumped" );
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
