using KeySwitchManager.Common.Text;
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
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var removedCount = 0;

            Presenter.Present( $"Removing keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName, instrumentName ) )
            {
                removedCount = Repository.Delete(
                    IDeveloperNameFactory.Default.Create( developerName ),
                    IProductNameFactory.Default.Create( productName ),
                    IInstrumentNameFactory.Default.Create( instrumentName )
                );
            }
            else if( !StringHelper.IsNullOrTrimEmpty( developerName, productName ) )
            {
                removedCount = Repository.Delete(
                    IDeveloperNameFactory.Default.Create( developerName ),
                    IProductNameFactory.Default.Create( productName )
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