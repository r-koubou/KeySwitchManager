using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Delete;

using RkHelper.Primitives;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class DeleteInteractor : IDeleteUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IDeletePresenter Presenter { get; }

        public DeleteInteractor( IKeySwitchRepository repository ) :
            this( repository, new IDeletePresenter.Null() )
        {}

        public DeleteInteractor(
            IKeySwitchRepository repository,
            IDeletePresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public async Task<DeleteResponse> ExecuteAsync( DeleteRequest request, CancellationToken cancellationToken )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var removedCount = 0;

            Presenter.Present( $"Removing keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                removedCount = await Repository.DeleteAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName ),
                    cancellationToken
                );
            }
            else if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                removedCount = await Repository.DeleteAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    cancellationToken
                );
            }

            if( removedCount > 0 )
            {
                Presenter.Present( $"{removedCount} record(s) removed" );
            }
            else
            {
                Presenter.Present( $"record not found" );
            }

            return new DeleteResponse( removedCount );
        }
    }
}
