using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class ImportController
    {
        public void Execute(
            IKeySwitchRepository repository,
            IContent content,
            IImportContentReader contentReader,
            IImportFilePresenter presenter )
            => ExecuteAsync( repository, content, contentReader, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            IKeySwitchRepository repository,
            IContent content,
            IImportContentReader contentReader,
            IImportFilePresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var interactor = new ImportInteractor( repository, presenter );
            var inputValue = new ImportInputValue( contentReader, content );
            var inputData = new ImportInputData( inputValue );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
