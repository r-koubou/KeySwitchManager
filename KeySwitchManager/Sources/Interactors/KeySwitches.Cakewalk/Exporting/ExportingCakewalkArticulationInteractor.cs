using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Gateways.KeySwitches.Helpers;
using KeySwitchManager.Presenters.KeySwitch.Cakewalk;
using KeySwitchManager.UseCases.KeySwitches.Cakewalk.Exporting;
using KeySwitchManager.UseCases.KeySwitches.Cakewalk.Translators;

namespace KeySwitchManager.Interactors.KeySwitches.Cakewalk.Exporting
{
    public class ExportingCakewalkArticulationInteractor : IExportingCakewalkArticulationUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchToCakewalkArticulationModel Translator { get; }
        private IExportingCakewalkArticulationPresenter Presenter { get; }

        public ExportingCakewalkArticulationInteractor(
            IKeySwitchRepository repository,
            IKeySwitchToCakewalkArticulationModel translator,
            IExportingCakewalkArticulationPresenter presenter )
        {
            Repository = repository;
            Translator = translator;
            Presenter  = presenter;
        }

        private ExportingCakewalkArticulationResponse CreateResponse( IEnumerable<KeySwitch> query )
        {
            var responseParam = new List<ExportingCakewalkArticulationResponse.Element>();

            foreach( var k in query )
            {
                var text = Translator.Translate( k );
                responseParam.Add( new ExportingCakewalkArticulationResponse.Element( k, text ) );
            }

            return new ExportingCakewalkArticulationResponse( responseParam );

        }

        public ExportingCakewalkArticulationResponse Execute( ExportingCakewalkArticulationRequest request )
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