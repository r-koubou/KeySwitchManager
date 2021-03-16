using System.Collections.Generic;

using KeySwitchManager.Gateways.KeySwitch;
using KeySwitchManager.Gateways.KeySwitch.Helper;
using KeySwitchManager.Presenters.VstExpressionMap;
using KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Exporting;
using KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Translations;

namespace KeySwitchManager.Interactors.KeySwitch.VstExpressionMap.Exporting
{
    public class ExportingVstExpressionMapInteractor : IExportingVstExpressionMapUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchToVstExpressionMapModel Translator { get; }
        private IExportingVstExpressionMapPresenter Presenter { get; }

        public ExportingVstExpressionMapInteractor(
            IKeySwitchRepository repository,
            IKeySwitchToVstExpressionMapModel translator,
            IExportingVstExpressionMapPresenter presenter )
        {
            Repository = repository;
            Translator = translator;
            Presenter  = presenter;
        }

        private ExportingVstExpressionMapResponse CreateResponse( IEnumerable<Domain.KeySwitches.KeySwitch> query )
        {
            var responseParam = new List<ExportingVstExpressionMapResponse.Element>();

            foreach( var k in query )
            {
                var text = Translator.Translate( k );
                responseParam.Add( new ExportingVstExpressionMapResponse.Element( k, text ) );
            }

            return new ExportingVstExpressionMapResponse( responseParam );

        }

        public ExportingVstExpressionMapResponse Execute( ExportingVstExpressionMapRequest request )
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