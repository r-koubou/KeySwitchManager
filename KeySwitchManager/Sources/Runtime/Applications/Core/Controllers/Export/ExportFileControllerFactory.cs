using System;

using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public class ExportFileControllerFactory : IExportControllerFactory
    {
        private string DatabaseFilePath { get; }
        private string OutputDirectory { get; }

        public ExportFileControllerFactory( string databaseFilePath, string outputDirectory )
        {
            DatabaseFilePath = databaseFilePath;
            OutputDirectory  = outputDirectory;
        }

        IController IExportControllerFactory.Create(
            string developerName,
            string productName,
            string instrumentName,
            ExportSupportedFormat format,
            ILogTextView logTextView )
        {
            var developer = new DeveloperName( developerName );
            var product = new ProductName( productName );
            var instrument = new InstrumentName( instrumentName );
            var outputDir = new DirectoryPath( OutputDirectory );

            var sourceDatabase = KeySwitchRepositoryFactory.CreateFileRepository( DatabaseFilePath );

            try
            {
                IController CreateImpl( IExportStrategy strategy )
                    => new ExportFileController( developer, product, instrument, sourceDatabase, strategy, new ExportFilePresenter( logTextView ) );

                IExportContentFactory contentFactory;
                IExportContentWriterFactory contentWriterFactory;
                IExportStrategy strategy;

                switch( format )
                {
                    case ExportSupportedFormat.Yaml:
                        contentFactory       = new YamlExportContentFactory();
                        contentWriterFactory = new YamlExportContentFileWriterFactory( new YamlGroupedExportPathBuilder( outputDir ) );
                        strategy             = new GroupedExportStrategy( contentWriterFactory, contentFactory );
                        break;

                    case ExportSupportedFormat.Xlsx:
                        contentFactory       = new ClosedXmlExportContentFactory();
                        contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( outputDir );
                        strategy             = new MultipleExportStrategy( contentWriterFactory, contentFactory );
                        break;

                    case ExportSupportedFormat.XlsxCombined:
                        contentFactory       = new ClosedXmlExportContentFactory();
                        contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( new ClosedXmlGroupedExportPathBuilder( outputDir ) );
                        strategy             = new GroupedExportStrategy( contentWriterFactory, contentFactory );
                        break;

                    case ExportSupportedFormat.Cubase:
                        contentFactory       = new CubaseExportContentFactory();
                        contentWriterFactory = new CubaseExportContentFileWriterFactory( outputDir );
                        strategy             = new MultipleExportStrategy( contentWriterFactory, contentFactory );
                        break;

                    case ExportSupportedFormat.StudioOne:
                        contentFactory       = new StudioOneExportContentFactory();
                        contentWriterFactory = new StudioOneExportContentFileWriterFactory( new StudioOneGroupedExportPathBuilder( outputDir ) );
                        strategy             = new GroupedExportStrategy( contentWriterFactory, contentFactory );
                        break;

                    case ExportSupportedFormat.Cakewalk:
                        contentFactory       = new CakewalkExportContentFactory();
                        contentWriterFactory = new CakewalkExportContentFileWriterFactory( outputDir );
                        strategy             = new MultipleExportStrategy( contentWriterFactory, contentFactory );
                        break;
                    default:
                        Disposer.Dispose( sourceDatabase );
                        throw new ArgumentException( $"Unsupported format :{format}" );
                }

                return CreateImpl( strategy );
            }
            catch
            {
                Disposer.Dispose( sourceDatabase );
                throw;
            }
        }
    }
}
