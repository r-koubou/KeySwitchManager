using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public sealed class ExportPresenter : IExportPresenter
    {
        public static readonly IExportPresenter Null = new NullImpl();

        private ILogTextView TextView { get; }

        public ExportPresenter( ILogTextView textView )
        {
            TextView = textView;
        }

        public async Task HandleAsync( ExportOutputData outputData, CancellationToken cancellationToken = default )
        {
            var keySwitches = outputData.Value.Result;
            var count = outputData.Value.WrittenCount;

            var developerName = outputData.Value.Input.DeveloperName;
            var productName = outputData.Value.Input.ProductName;
            var instrumentName = outputData.Value.Input.InstrumentName;

            if( !keySwitches.Any() )
            {
                TextView.Append( $"No keyswitch(es) found. ({nameof( developerName )}={developerName}, {nameof( productName )}={productName}, {nameof( instrumentName )}={instrumentName})" );
            }

            TextView.Append( $"{count} exported" );

            await Task.CompletedTask;
        }

        public async Task HandleExportedAsync( KeySwitch exported, CancellationToken cancellationToken = default )
        {
            TextView.Append( $"Exported: {exported}" );
            await Task.CompletedTask;
        }

        #region NullObject
        private class NullImpl : IExportPresenter
        {
            public async Task HandleAsync( ExportOutputData outputData, CancellationToken cancellationToken = default )
                => await Task.CompletedTask;

            public async Task HandleExportedAsync( KeySwitch exported, CancellationToken cancellationToken = default )
                => await Task.CompletedTask;
        }
        #endregion
    }
}
