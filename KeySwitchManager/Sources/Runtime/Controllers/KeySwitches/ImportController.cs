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
            IContent content,
            IImportContentReader contentReader,
            IKeySwitchRepository repository,
            IImportFilePresenter presenter )
            => ExecuteAsync( content, contentReader, repository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            IContent content,
            IImportContentReader contentReader,
            IKeySwitchRepository repository,
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
