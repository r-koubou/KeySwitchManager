using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.Gateways.KeySwitch.Helper;
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

            return CreateResponse(
                SearchHelper.Search( Repository, request.Guid, developerName, productName, instrumentName )
            );
        }
    }
}