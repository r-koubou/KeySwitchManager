using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Interactors.Helpers;
using KeySwitchManager.Presenters.VstExpressionMap;
using KeySwitchManager.UseCases.VstExpressionMap.Exporting;
using KeySwitchManager.UseCases.VstExpressionMap.Translations;

namespace KeySwitchManager.Interactors.VstExpressionMap.Exporting
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

        private ExportingVstExpressionMapResponse CreateResponse( IEnumerable<KeySwitch> query )
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