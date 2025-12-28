using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.KeySwitches.Controllers.Extensions
{
    public static class ExportControllerExtension
    {
        #region Export to local file
        public static void ExportToLocalFile(
            this ExportController me,
            string databasePath,
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IExportPresenter presenter )
            => ExportToLocalFileAsync( me, databasePath, developerName, productName, instrumentName, outputDirectory, format, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExportToLocalFileAsync(
            this ExportController me,
            string databasePath,
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var strategy = ExportStrategyFactory.CreateForDirectory( outputDirectory, format );
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );

            await me.ExportAsync( repository, developerName, productName, instrumentName, strategy, presenter, cancellationToken );
        }
        #endregion

        #region Export to specified stream
        public static void ExportToStream(
            this ExportController me,
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IExportPresenter presenter )
            => ExportToStreamAsync( me, repository, developerName, productName, instrumentName, outputStream, format, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExportToStreamAsync(
            this ExportController me,
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IExportPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var strategy = ExportStrategyFactory.CreateForStream( outputStream, format );

            await me.ExportAsync( repository, developerName, productName, instrumentName, strategy, presenter, cancellationToken );
        }
        #endregion
    }
}
