using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Searching;

namespace KeySwitchManager.Interactors.KeySwitches.Searching
{
    public class SearchingInteractor : ISearchingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private ISearchingPresenter Presenter { get; }

        public SearchingInteractor(
            IKeySwitchRepository repository,
            ISearchingPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public SearchingResponse Execute( SearchingRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            Presenter.Present( $"Searching keyswitch: Developer={developerName}, Product={productName}, Instrument={instrumentName}" );

            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName, instrumentName ) )
            {
                var result = Repository.Find(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName )
                );
                return new SearchingResponse( result );
            }
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName ) )
            {
                var result = Repository.Find(
                    new DeveloperName( developerName ),
                    new ProductName( productName )
                );
                return new SearchingResponse( result );
            }

            return new SearchingResponse( new List<KeySwitch>() );
        }
    }
}