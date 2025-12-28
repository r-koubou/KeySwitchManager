using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public sealed class DumpInteractor : IDumpUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IExportStrategy Strategy { get; }
        private IDumpPresenter Presenter { get; }

        public DumpInteractor(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IDumpPresenter presenter )
        {
            Repository = repository;
            Strategy   = strategy;
            Presenter  = presenter;
        }

        public async Task HandleAsync( UnitInputData inputData, CancellationToken cancellationToken = default )
        {
            var all = await Repository.FindAllAsync( cancellationToken );

            var sorted = all.OrderBy( x => x.DeveloperName.Value )
               .ThenBy( x => x.ProductName.Value )
               .ThenBy( x => x.InstrumentName.Value ).ToList();

            var dumpedCount = sorted.Count;

            if( dumpedCount > 0 )
            {
                await Strategy.ExportAsync( sorted, cancellationToken );
            }

            var output = new DumpOutputData( true, new DumpOutputValue( dumpedCount ) );

            await Presenter.HandleAsync( output, cancellationToken );
        }
    }
}
