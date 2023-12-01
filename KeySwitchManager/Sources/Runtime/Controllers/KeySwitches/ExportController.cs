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
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( developerName, productName, instrumentName, strategy, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var interactor = new ExportInteractor(
                sourceRepository,
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
