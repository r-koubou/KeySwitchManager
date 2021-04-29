using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class SpreadSheetImportInteractor : ISpreadSheetImportUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchRepository InputRepository { get; }

        private ISpreadsheetImportPresenter Presenter { get; }

        public SpreadSheetImportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository ) :
            this( repository, inputRepository, new ISpreadsheetImportPresenter.Null() )
        {}

        public SpreadSheetImportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository,
            ISpreadsheetImportPresenter presenter )
        {
            Repository      = repository;
            InputRepository = inputRepository;
            Presenter       = presenter;
        }

        public SpreadsheetImportResponse Execute( SpreadsheetImportRequest request )
        {
            var keySwitches = InputRepository.FindAll();
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                Presenter.Message( $"... {i.ProductName} | {i.InstrumentName}" );

                var r = Repository.Save( i );
                insertedCount += r.Inserted;
                updatedCount  += r.Updated;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new SpreadsheetImportResponse( keySwitches );
        }
    }
}