using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitch.Removing;

using RkHelper.Text;

namespace KeySwitchManager.Interactors.KeySwitch.Removing
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

            return new RemovingResponse( removedCount );
        }
    }
}