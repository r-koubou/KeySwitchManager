using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Export
{
    public sealed class ExportController
    {
        public void Execute(
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportSupportedFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
                => ExecuteAsync( developerName, productName, instrumentName, outputDirectory, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportSupportedFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var strategy = StrategyFactory.Create( outputDirectory, format );
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
