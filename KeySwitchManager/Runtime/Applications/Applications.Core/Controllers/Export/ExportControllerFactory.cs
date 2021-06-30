using System;
using System.Collections.Generic;

using Application.Core.Views.LogView;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Json.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Xml.Cubase;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne;

using RkHelper.System;

namespace Application.Core.Controllers.Export
{
    public static class ExportControllerFactory
    {
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
                Disposer.Dispose( sourceDatabase );
                throw;
            }

            Disposer.Dispose( sourceDatabase );
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
