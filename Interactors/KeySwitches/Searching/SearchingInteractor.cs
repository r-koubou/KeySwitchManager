using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Searching;
using KeySwitchManager.UseCases.KeySwitches.Translations;

using RkHelper.Text;

namespace KeySwitchManager.Interactors.KeySwitches.Searching
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

        private SearchingResponse CreateResponse( IReadOnlyCollection<KeySwitch> query )
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
                    IDeveloperNameFactory.Default.Create( request.DeveloperName ),
                    IProductNameFactory.Default.Create( request.ProductName ),
                    IInstrumentNameFactory.Default.Create( request.InstrumentName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                var keySwitches = Repository.Find(
                    IDeveloperNameFactory.Default.Create( request.DeveloperName ),
                    IProductNameFactory.Default.Create( request.ProductName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                var keySwitches = Repository.Find(
                    IDeveloperNameFactory.Default.Create( request.DeveloperName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            return new SearchingResponse( new List<KeySwitch>(), PlainText.Empty );
        }
    }
}