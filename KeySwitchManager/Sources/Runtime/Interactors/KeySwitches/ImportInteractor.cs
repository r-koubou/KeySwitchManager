using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ImportInteractor : IImportUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IImportFilePresenter Presenter { get; }

        public ImportInteractor(
            IKeySwitchRepository repository,
            IImportFilePresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public async Task HandleAsync( ImportInputData inputData, CancellationToken cancellationToken = default )
        {
            var inputValue = inputData.Value;
            var insertedCount = 0;
            var updatedCount = 0;

            var keySwitches = await inputValue.ContentReader.ReadAsync( inputValue.Content, cancellationToken );

            foreach( var i in keySwitches )
            {
                if( cancellationToken.IsCancellationRequested )
                {
                    break;
                }

                var r = await Repository.SaveAsync( i, cancellationToken );
                updatedCount  += r.Updated;
                insertedCount += r.Inserted;

                await Presenter.HandleImportedAsync( i, cancellationToken );
            }

            if( insertedCount > 0 || updatedCount > 0 )
            {
                await Repository.FlushAsync( cancellationToken );
            }

            var outputData = new ImportOutputData( true, new ImportOutputValue( insertedCount, updatedCount ) );

            await Presenter.HandleAsync( outputData, cancellationToken );
        }
    }
}
