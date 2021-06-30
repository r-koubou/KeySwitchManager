using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ImportSpreadSheetInteractor : IImportSpreadSheetUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchRepository InputRepository { get; }

        private IImportSpreadsheetPresenter Presenter { get; }

        public ImportSpreadSheetInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository ) :
            this( repository, inputRepository, new IImportSpreadsheetPresenter.Null() )
        {}

        public ImportSpreadSheetInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository,
            IImportSpreadsheetPresenter presenter )
        {
            Repository      = repository;
            InputRepository = inputRepository;
            Presenter       = presenter;
        }

        public ImportSpreadSheetResponse Execute( ImportSpreadSheetRequest request )
        {
            var keySwitches = InputRepository.FindAll();
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                Presenter.Present( $"... {i.ProductName} | {i.InstrumentName}" );

                var r = Repository.Save( i );
                insertedCount += r.Inserted;
                updatedCount  += r.Updated;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportSpreadSheetResponse( keySwitches );
        }
    }
}