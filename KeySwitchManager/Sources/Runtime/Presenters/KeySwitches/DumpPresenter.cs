using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Presenters
{
    public sealed class DumpPresenter : IDumpPresenter
    {
        public static readonly IDumpPresenter Null = new NullImpl();

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
        private class NullImpl : IDumpPresenter
        {
            public async Task HandleAsync( DumpOutputData outputData, CancellationToken cancellationToken = default )
                => await Task.CompletedTask;
        }
        #endregion
    }
}
