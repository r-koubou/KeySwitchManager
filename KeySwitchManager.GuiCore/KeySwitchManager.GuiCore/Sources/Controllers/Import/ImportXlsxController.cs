using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public class ImportXlsxController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private LoadOnlyKeySwitchFileRepository SpreadSheetFileRepository { get; }
        private ISpreadsheetImportPresenter Presenter { get; }

        #region Ctor
        public ImportXlsxController(
            IKeySwitchRepository databaseRepository,
            LoadOnlyKeySwitchFileRepository spreadSheetFileRepository,
            ISpreadsheetImportPresenter presenter )
        {
            DatabaseRepository        = databaseRepository;
            SpreadSheetFileRepository = spreadSheetFileRepository;
            Presenter                 = presenter;
        }
        #endregion

        public void Dispose()
        {
            try
            {
                DatabaseRepository.Dispose();
            }
            catch( Exception e )
            {
                // ignored
            }
        }

        public void Execute()
        {
            var interactor = new SpreadSheetImportInteractor( DatabaseRepository, SpreadSheetFileRepository, Presenter );
            var request = new SpreadsheetImportRequest();
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }
    }
}
