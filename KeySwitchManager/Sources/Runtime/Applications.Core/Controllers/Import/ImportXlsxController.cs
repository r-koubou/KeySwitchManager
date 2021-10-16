using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public class ImportXlsxController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private LoadOnlyKeySwitchFileRepository SpreadSheetFileRepository { get; }
        private IImportSpreadsheetPresenter Presenter { get; }

        #region Ctor
        public ImportXlsxController(
            IKeySwitchRepository databaseRepository,
            LoadOnlyKeySwitchFileRepository spreadSheetFileRepository,
            IImportSpreadsheetPresenter presenter )
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
            catch
            {
                // ignored
            }
        }

        public void Execute()
        {
            var interactor = new ImportSpreadSheetInteractor( DatabaseRepository, SpreadSheetFileRepository, Presenter );
            var request = new ImportSpreadSheetRequest();
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }
    }
}
