using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Applications.Standalone.Core.Presenters
{
    public class ImportPresenter : IImportFilePresenter
    {
        public static readonly IImportFilePresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public ImportPresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleAsync( ImportOutputData outputData, CancellationToken cancellationToken = default )
        {
            var insertedCount = outputData.Value.InsertedCount;
            var updatedCount = outputData.Value.UpdatedCount;

            TextView.Append( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );
            TextView.Append( "Complete" );
            await Task.CompletedTask;
        }

        public async Task HandleImportedAsync( KeySwitch keySwitch, CancellationToken cancellationToken = default )
        {
            TextView.Append( $"{keySwitch}" );
            await Task.CompletedTask;
        }

        #region NullObject
        private class NullImpl : IImportFilePresenter
        {
            public async Task HandleAsync( ImportOutputData outputData, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }

            public async Task HandleImportedAsync( KeySwitch keySwitch, CancellationToken cancellationToken = default )
            {
                await Task.CompletedTask;
            }
        }
        #endregion
    }
}
