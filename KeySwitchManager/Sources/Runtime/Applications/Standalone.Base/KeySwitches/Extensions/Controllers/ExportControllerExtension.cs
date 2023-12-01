using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Extensions.Controllers
{
    public static class ExportControllerExtension
    {
        #region Export to local file
        public static void Execute(
            this ExportController me,
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( me, developerName, productName, instrumentName, outputDirectory, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
            this ExportController me,
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

            await me.ExecuteAsync( developerName, productName, instrumentName, strategy, sourceRepository, presenter, cancellationToken );
        }
        #endregion

        #region Export to specified stream
        public static void Execute(
            this ExportController me,
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IKeySwitchRepository sourceRepository,
            IExportPresenter presenter )
            => ExecuteAsync( me, developerName, productName, instrumentName, outputStream, format, sourceRepository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
            this ExportController me,
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

            await me.ExecuteAsync( developerName, productName, instrumentName, strategy, sourceRepository, presenter, cancellationToken );
        }
        #endregion
    }
}
