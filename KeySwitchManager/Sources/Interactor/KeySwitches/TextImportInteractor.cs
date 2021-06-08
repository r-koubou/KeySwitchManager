using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class TextImportInteractor : ITextImportUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchRepository InputRepository { get; }
        private ITextImportPresenter Presenter { get; }

        public TextImportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository ) :
            this( repository, inputRepository, new ITextImportPresenter.Null() )
        {}

        public TextImportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository inputRepository,
            ITextImportPresenter presenter )
        {
            Repository           = repository;
            Presenter            = presenter;
            InputRepository = inputRepository;
        }

        public TextImportResponse Execute( TextImportRequest request )
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

            return new TextImportResponse( updatedCount );
        }
    }
}