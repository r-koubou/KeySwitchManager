using System;
using System.Collections.Generic;

using KeySwitchManager.AppCore.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Json.Cakewalk;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Xml.Cubase;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne;

namespace KeySwitchManager.AppCore.Controllers.Export
{
    public static class ExportControllerFactory
    {
        private static void DisposeSafety( IDisposable disposable )
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        public static IController Create(
            string developerName,
            string productName,
            string instrumentName,
            string databaseFile,
            string outputDirectory,
            ExportSupportedFormat format,
            ILogTextView logTextView )
        {
            var developer = new DeveloperName( developerName );
            var product = new ProductName( productName );
            var instrument = new InstrumentName( instrumentName );
            var databasePath = new FilePath( databaseFile );
            var outputDir = new DirectoryPath( outputDirectory );

            var sourceDatabase = new LiteDbKeySwitchRepository( databasePath );

            IController CreateDawController( IKeySwitchRepository targetFileRepository )
            {
                return new ExportDawController( developer!, product!, instrument!, sourceDatabase!, targetFileRepository, new ExportDawPresenter( logTextView ) );
            }

            try
            {
                switch( format )
                {
                    case ExportSupportedFormat.Xlsx:
                        var xlsxRepository = new ClosedXmlFileSaveRepository( outputDir, new FileAccessListener( logTextView ) );
                        return new ExportXlsxController( developer, product, sourceDatabase, xlsxRepository, new ExportXlsxPresenter( logTextView ) );

                    case ExportSupportedFormat.Cubase:
                        var cubaseRepository = new CubaseFileRepository( outputDir );
                        return CreateDawController( cubaseRepository );

                    case ExportSupportedFormat.StudioOne:
                        var studioOneRepository = new StudioOneFileRepository( outputDir );
                        return CreateDawController( studioOneRepository );

                    case ExportSupportedFormat.Cakewalk:
                        var cakewalkRepository = new CakewalkFileRepository( outputDir );
                        return CreateDawController( cakewalkRepository );
                }
            }
            catch
            {
                DisposeSafety( sourceDatabase );
                throw;
            }

            DisposeSafety( sourceDatabase );
            throw new ArgumentException( $"{format} is not supported" );
        }

        private class FileAccessListener : IStorageAccessListener
        {
            private ILogTextView LogTextView { get; }

            public FileAccessListener( ILogTextView logTextView )
            {
                LogTextView = logTextView;
            }

            public void OnWriteAccess( IReadOnlyCollection<KeySwitch> keySwitches, IPath path )
            {
                foreach( var x in keySwitches )
                {
                    LogTextView.Append( $"{x.DeveloperName} {x.ProductName} {x.InstrumentName}" );
                }
            }
        }

    }
}
