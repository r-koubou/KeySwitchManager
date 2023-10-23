using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Dump;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class DumpFileInteractor : IDumpFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchWriter Writer { get; }
        private IDumpFilePresenter Presenter { get; }

        public DumpFileInteractor(
            IKeySwitchRepository repository, IKeySwitchWriter writer ) :
            this( repository, writer, new IDumpFilePresenter.Null() )
        {}

        public DumpFileInteractor(
            IKeySwitchRepository repository,
            IKeySwitchWriter writer,
            IDumpFilePresenter presenter )
        {
            Repository = repository;
            Writer     = writer;
            Presenter  = presenter;
        }

        async Task<DumpFileResponse> IDumpFileUseCase.ExecuteAsync( DumpFileRequest request )
        {
            var all = await Repository.FindAllAsync();

            var sorted = all.OrderBy( x => x.DeveloperName.Value )
               .ThenBy( x => x.ProductName.Value )
               .ThenBy( x => x.InstrumentName.Value ).ToList();

            if( sorted.Count > 0 )
            {
                await Writer.WriteAsync( sorted, null );
                return new DumpFileResponse( sorted.Count );
            }

            Presenter.Present( $"No keyswitch(es) found." );
            return new DumpFileResponse( 0 );
        }
    }
}
