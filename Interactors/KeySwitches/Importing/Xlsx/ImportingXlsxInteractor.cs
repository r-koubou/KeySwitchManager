using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx;

namespace KeySwitchManager.Interactors.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxInteractor : IImportingXlsxUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchXlsxRepository SpreadSheetRepository { get; }
        private IImportingXlsxPresenter Presenter { get; }

        public ImportingXlsxInteractor(
            IKeySwitchRepository repository,
            IKeySwitchXlsxRepository spreadSheetRepository,
            IImportingXlsxPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
            SpreadSheetRepository = spreadSheetRepository;
        }

        public ImportingXlsxResponse Execute( ImportingXlsxRequest request )
        {
            var keySwitches = SpreadSheetRepository.Load();
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                var r = Repository.Save( i );
                insertedCount += r.Inserted;
                updatedCount  += r.Updated;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportingXlsxResponse( keySwitches );
        }
    }
}