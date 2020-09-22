using System.Collections.Generic;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.StudioOneKeySwitch;
using KeySwitchManager.UseCases.StudioOneKeySwitch.Exporting;
using KeySwitchManager.UseCases.StudioOneKeySwitch.Translations;

namespace KeySwitchManager.Interactors.StudioOneKeySwitch.Exporting
{
    public class ExportingStudioOneKeySwitchInteractor : IExportingStudioOneKeySwitchUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchToStudioOneKeySwitchModel Translator { get; }
        private IExportingStudioOneKeySwitchPresenter Presenter { get; }

        public ExportingStudioOneKeySwitchInteractor(
            IKeySwitchRepository repository,
            IKeySwitchToStudioOneKeySwitchModel translator,
            IExportingStudioOneKeySwitchPresenter presenter )
        {
            Repository = repository;
            Translator = translator;
            Presenter  = presenter;
        }

        private ExportingStudioOneKeySwitchResponse CreateResponse( IEnumerable<KeySwitch> query )
        {
            var responseParam = new List<ExportingStudioOneKeySwitchResponse.Element>();

            foreach( var k in query )
            {
                var text = Translator.Translate( k );
                responseParam.Add( new ExportingStudioOneKeySwitchResponse.Element( k, text ) );
            }

            return new ExportingStudioOneKeySwitchResponse( responseParam );

        }

        public ExportingStudioOneKeySwitchResponse Execute( ExportingStudioOneKeySwitchRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            #region By Guid
            if( request.Guid != default )
            {
                return CreateResponse(
                    Repository.Find( new EntityGuid( request.Guid ) )
                );
            }
            #endregion

            #region By Developer, Product, Instrument
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName, instrumentName ) )
            {
                return CreateResponse(
                    Repository.Find(
                        new DeveloperName( request.DeveloperName ),
                        new ProductName( request.ProductName ),
                        new InstrumentName( request.InstrumentName ) )
                );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsNullOrTrimEmpty( developerName, productName ) )
            {
                return CreateResponse(
                    Repository.Find(
                        new DeveloperName( request.DeveloperName ),
                        new ProductName( request.ProductName ) )
                );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsNullOrTrimEmpty( developerName ) )
            {
                return CreateResponse(
                    Repository.Find(
                        new DeveloperName( request.DeveloperName ) )
                );
            }
            #endregion

            // Record not found
            return new ExportingStudioOneKeySwitchResponse(
                new ExportingStudioOneKeySwitchResponse.Element[] {}
            );

        }
    }
}