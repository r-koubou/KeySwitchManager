using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class DumpFileInteractor : IDumpFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IExportStrategy Strategy { get; }
        private IDumpFilePresenter Presenter { get; }

        public DumpFileInteractor(
            IKeySwitchRepository repository, IExportStrategy strategy ) :
            this( repository, strategy, new IDumpFilePresenter.Null() ) {}

        public DumpFileInteractor(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IDumpFilePresenter presenter )
        {
            Repository = repository;
            Strategy     = strategy;
            Presenter  = presenter;
        }

        public async Task<DumpFileResponse> ExecuteAsync( DumpFileRequest request, CancellationToken cancellationToken )
        {
            var all = await Repository.FindAllAsync( cancellationToken );

            var sorted = all.OrderBy( x => x.DeveloperName.Value )
               .ThenBy( x => x.ProductName.Value )
               .ThenBy( x => x.InstrumentName.Value ).ToList();

            if( sorted.Count > 0 )
            {
                await Strategy.ExportAsync( sorted, cancellationToken );
                return new DumpFileResponse( sorted.Count );
            }

            Presenter.Present( $"No keyswitch(es) found." );
            return new DumpFileResponse( 0 );
        }
    }
}
