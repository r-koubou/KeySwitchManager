using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers
{
    public sealed class ExportController
    {
        #region Export with specified strategy
        public void Execute(
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( developerName, productName, instrumentName, strategy, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string productName,
            string instrumentName,
            IExportStrategy strategy,
            ExportFormat format,
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
        #endregion

        #region Export to local file
        public void Execute(
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( developerName, productName, instrumentName, outputDirectory, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var strategy = ExportStrategyFactory.CreateForDirectory( outputDirectory, format );

            await ExecuteAsync( developerName, productName, instrumentName, strategy, format, sourceRepository, presenter, cancellationToken );
        }
        #endregion

        #region Export to specified stream
        public void Execute(
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( developerName, productName, instrumentName, outputStream, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var strategy = ExportStrategyFactory.CreateForStream( outputStream, format );

            await ExecuteAsync( developerName, productName, instrumentName, strategy, format, sourceRepository, presenter, cancellationToken );
        }
        #endregion
    }
}
