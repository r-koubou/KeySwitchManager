using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Delete;

using RkHelper.Text;

namespace KeySwitchManager.Interactor.KeySwitches
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

        public DeleteResponse Execute( DeleteRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var removedCount = 0;

            Presenter.Present( $"Removing keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                removedCount = Repository.Delete(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName )
                );
            }
            else if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                removedCount = Repository.Delete(
                    new DeveloperName( developerName ),
                    new ProductName( productName )
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