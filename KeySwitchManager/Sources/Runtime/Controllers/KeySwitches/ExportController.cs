using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class ExportController
    {
        public void Execute(
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            IExportPresenter presenter )
            => ExecuteAsync( repository, developerName, productName, instrumentName, strategy, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var interactor = new ExportInteractor(
                repository,
                strategy,
                presenter
            );

            var inputValue = new ExportInputValue(
                developerName,
                productName,
                instrumentName
            );

            await interactor.HandleAsync( new ExportInputData( inputValue ), cancellationToken );
        }
    }
}
