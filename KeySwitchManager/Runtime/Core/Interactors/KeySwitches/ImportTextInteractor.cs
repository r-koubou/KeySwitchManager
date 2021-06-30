using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ImportTextInteractor : IImportTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchRepository InputRepository { get; }
        private IImportTextPresenter Presenter { get; }

        public ImportTextInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository ) :
            this( repository, inputRepository, new IImportTextPresenter.Null() )
        {}

        public ImportTextInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository,
            IImportTextPresenter presenter )
        {
            Repository           = repository;
            Presenter            = presenter;
            InputRepository = inputRepository;
        }

        public ImportTextResponse Execute( ImportTextRequest request )
        {
            var keySwitches = InputRepository.FindAll();
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                Presenter.Present( $"... {i.ProductName} | {i.InstrumentName}" );

                var r = Repository.Save( i );
                updatedCount += r.Updated;
                insertedCount += r.Inserted;
            }

            var flushed = Repository.Flush();

            if( flushed == 0 )
            {
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({Repository.GetType()})" );
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportTextResponse( updatedCount );
        }
    }
}