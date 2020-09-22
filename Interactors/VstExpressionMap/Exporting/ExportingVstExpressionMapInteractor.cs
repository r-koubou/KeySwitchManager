using System.Collections.Generic;

using KeySwitchManager.Common.Utilities;
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Gateways.KeySwitches;
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
            return new ExportingVstExpressionMapResponse(
                new ExportingVstExpressionMapResponse.Element[] {}
            );

        }
    }
}