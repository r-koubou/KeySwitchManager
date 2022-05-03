using System;
using System.IO;

using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Export
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

            var sourceDatabase = new LiteDbRepository( databasePath );

            try
            {
                IController CreateImpl( IKeySwitchWriter writer )
                    => new ExportFileController( developer, product, instrument, sourceDatabase, writer, new ExportFilePresenter( logTextView ) );

                switch( format )
                {
                    case ExportSupportedFormat.Yaml:
                        return CreateImpl( new MultipleYamlFileWriter( outputDir ) );

                    case ExportSupportedFormat.Xlsx:
                        return CreateImpl( new MultipleClosedXmlWriter( outputDir ) );

                    case ExportSupportedFormat.XlsxCombined:
                        return CreateImpl( new CombinedClosedXmlWriter( outputDir ) );

                    case ExportSupportedFormat.Cubase:
                        return CreateImpl( new MultipleCubaseWriter( outputDir ) );

                    case ExportSupportedFormat.StudioOne:
                        return CreateImpl( new StudioOneWriter( outputDir ) );

                    case ExportSupportedFormat.Cakewalk:
                        return CreateImpl( new MultipleCakewalkWriter( outputDir ) );
                    case ExportSupportedFormat.Dump:
                        outputDir.CreateNew();
                        var timeStamp = DateTime.Now.ToString( "yyyyMMdd-HHmmss" );
                        var dumpPath = new FilePath( Path.Combine( outputDir.Path, $"dump@{timeStamp}.yaml" ) );
                        var dumpStream = dumpPath.OpenStream( FileMode.Create, FileAccess.Write );
                        return CreateImpl( new YamlKeySwitchWriter( dumpStream ) );
                    default:
                        Disposer.Dispose( sourceDatabase );
                        throw new ArgumentException( $"Unsupported format :{format}" );
                }
            }
            catch
            {
                Disposer.Dispose( sourceDatabase );
                throw;
            }
        }
    }
}
