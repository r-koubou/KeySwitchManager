using System;

using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting
{
    public class ExportingXlsxInteractor : IExportingXlsxUseCase
    {
        private IKeySwitchSpreadSheetRepository Repository { get; }

        public ExportingXlsxInteractor( IKeySwitchSpreadSheetRepository repository )
        {
            Repository = repository;
        }

        public ExportingXlsxResponse Execute( ExportingXlsxRequest request )
        {
            var result = Repository.Save( request.KeySwitches );
            return new ExportingXlsxResponse( result );
        }
    }
}