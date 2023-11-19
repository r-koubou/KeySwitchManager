using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
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

        public async Task<ImportFileResponse> ExecuteAsync( ImportFileRequest request, CancellationToken cancellationToken )
        {
            var insertedCount = 0;
            var updatedCount = 0;

            var keySwitches = await request.ContentReader.ReadAsync( request.Content, cancellationToken );

            foreach( var i in keySwitches )
            {
                if( cancellationToken.IsCancellationRequested )
                {
                    break;
                }

                var r = await Repository.SaveAsync( i, cancellationToken );
                updatedCount  += r.Updated;
                insertedCount += r.Inserted;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportFileResponse( insertedCount, updatedCount );

        }
    }
}
