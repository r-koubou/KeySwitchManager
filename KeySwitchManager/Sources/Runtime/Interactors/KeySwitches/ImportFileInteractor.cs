using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ImportFileInteractor : IImportFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IImportFilePresenter Presenter { get; }

        public ImportFileInteractor(
            IKeySwitchRepository repository ) :
            this( repository, new IImportFilePresenter.Null() )
        {}

        public ImportFileInteractor(
            IKeySwitchRepository repository,
            IImportFilePresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public ImportFileResponse Execute( ImportFileRequest request )
        {
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in request.KeySwitches )
            {
                var r = Repository.Save( i );
                updatedCount  += r.Updated;
                insertedCount += r.Inserted;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportFileResponse( insertedCount, updatedCount );

        }
    }
}