using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting
{
    public class ExportingJsonInteractor : IExportingTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchListToJsonListText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingJsonInteractor(
            IKeySwitchRepository repository,
            IKeySwitchListToJsonListText translator,
            IExportingTextPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        private ExportingTextResponse CreateResponse( IEnumerable<KeySwitch> query )
        {
            var keySwitches = query.ToList();
            return new ExportingTextResponse( Translator.Translate( keySwitches ), keySwitches.Count );
        }

        public ExportingTextResponse Execute( ExportingTextRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            #region By Guid
            if( request.Guid != default )
            {
                var keySwitches = Repository.Find( new EntityGuid( request.Guid ) );
                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName, instrumentName ) )
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
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsNullOrTrimEmpty( developerName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName )
                );

                return CreateResponse( keySwitches );
            }
            #endregion

            return new ExportingTextResponse();
        }
    }
}