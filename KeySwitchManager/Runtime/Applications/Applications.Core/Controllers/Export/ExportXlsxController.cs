using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet;

using RkHelper.System;

namespace Application.Core.Controllers.Export
{
    public class ExportXlsxController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IKeySwitchRepository TargetRepository { get; }
        private DeveloperName DeveloperName { get; }
        private ProductName ProductName { get; }
        private IExportSpreadsheetPresenter Presenter { get; }

        public ExportXlsxController(
            DeveloperName developerName,
            ProductName productName,
            IKeySwitchRepository sourceRepository,
            IKeySwitchRepository targetRepository,
            IExportSpreadsheetPresenter presenter )
        {
            SourceRepository = sourceRepository;
            Presenter        = presenter;
            DeveloperName    = developerName;
            ProductName      = productName;
            TargetRepository = targetRepository;
        }

        public void Dispose()
        {
            Disposer.Dispose( TargetRepository );
            Disposer.Dispose( SourceRepository );
        }

        public void Execute()
        {
            var info = new KeySwitchInfo( DeveloperName.Value, ProductName.Value );
            var keySwitches = SearchHelper.Search( SourceRepository, info );

            var interactor = new ExportSpreadsheetInteractor(
                TargetRepository,
                Presenter
            );

            var response = interactor.Execute( new ExportSpreadsheetRequest( keySwitches ) );
            Presenter.Complete( response );
        }
    }
}
