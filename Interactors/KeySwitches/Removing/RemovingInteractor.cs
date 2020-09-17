using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.Interactors.KeySwitches.Removing
{
    public class RemovingInteractor : IRemovingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IRemovingPresenter Presenter { get; }

        public RemovingInteractor(
            IKeySwitchRepository repository,
            IRemovingPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public RemovingResponse Execute( RemovingRequest request )
        {
            var developerName = new DeveloperName( request.DeveloperName );
            var productName = new ProductName( request.ProductName );
            var instrumentName = new InstrumentName( request.InstrumentName );

            Presenter.Present( $"Removing keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            var removedCount = Repository.Delete( developerName, productName, instrumentName );
            Presenter.Present( $"{removedCount} record(s) removed" );

            return new RemovingResponse( removedCount );
        }
    }
}