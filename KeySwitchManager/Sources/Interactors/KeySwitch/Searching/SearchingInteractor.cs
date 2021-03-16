using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitch.Searching;
using KeySwitchManager.UseCases.KeySwitch.Translations;

using RkHelper.Text;

namespace KeySwitchManager.Interactors.KeySwitch.Searching
{
    public class SearchingInteractor : ISearchingUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchListToJsonListText Translator { get; }
        private ISearchingPresenter Presenter { get; }

        public SearchingInteractor(
            IKeySwitchRepository repository,
            IKeySwitchListToJsonListText translator,
            ISearchingPresenter presenter )
        {
            Repository = repository;
            Translator = translator;
            Presenter  = presenter;
        }

        private SearchingResponse CreateResponse( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> query )
        {
            return new SearchingResponse( query, Translator.Translate( query ) );
        }

        public SearchingResponse Execute( SearchingRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName ),
                    new InstrumentName( request.InstrumentName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            return new SearchingResponse( new List<Domain.KeySwitches.KeySwitch>(), PlainText.Empty );
        }
    }
}