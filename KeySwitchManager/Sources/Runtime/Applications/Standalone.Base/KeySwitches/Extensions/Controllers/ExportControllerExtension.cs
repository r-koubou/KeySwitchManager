﻿using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Base.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Base.KeySwitches.Extensions.Controllers
{
    public static class ExportControllerExtension
    {
        #region Export to local file
        public static void Execute(
            this ExportController me,
            string databasePath,
            string developerName,
            string productName,
            string instrumentName,
            string outputDirectory,
            ExportFormat format,
            IExportPresenter presenter )
            => ExecuteAsync( me, databasePath, developerName, productName, instrumentName, outputDirectory, format, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
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

            await me.ExecuteAsync( repository, developerName, productName, instrumentName, strategy, presenter, cancellationToken );
        }
        #endregion

        #region Export to specified stream
        public static void Execute(
            this ExportController me,
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            Stream outputStream,
            ExportFormat format,
            IExportPresenter presenter )
            => ExecuteAsync( me, repository, developerName, productName, instrumentName, outputStream, format, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
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

            await me.ExecuteAsync( repository, developerName, productName, instrumentName, strategy, presenter, cancellationToken );
        }
        #endregion
    }
}
