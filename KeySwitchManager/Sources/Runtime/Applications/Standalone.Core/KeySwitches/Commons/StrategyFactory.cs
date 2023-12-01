using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons
{
    public static class StrategyFactory
    {
        public static IExportStrategy Create( string outputFilePath )
        {
            var fileName = outputFilePath.ToLower();

            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory;
            IExportStrategy strategy;

            if( fileName.EndsWith( ".xlsx" ) )
            {
                contentFactory       = new ClosedXmlExportContentFactory();
                contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( new SpecificExportPathBuilder( new FilePath( outputFilePath ) ) );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else if( fileName.EndsWith( ".yaml" ) || fileName.EndsWith( ".yml" ) )
            {
                contentFactory       = new YamlExportContentFactory();
                contentWriterFactory = new YamlExportContentFileWriterFactory( new SpecificExportPathBuilder( new FilePath( outputFilePath ) ) );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else
            {
                throw new ArgumentException( $"{outputFilePath} is unknown file format" );
            }

            return strategy;
        }

        public static IExportStrategy Create( string outputDirectory, ExportSupportedFormat format )
        {
            var outputDir = new DirectoryPath( outputDirectory );
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

                case ExportSupportedFormat.Logic:
                    contentFactory       = new LogicExportContentFactory();
                    contentWriterFactory = new LogicExportContentFileWriterFactory( outputDir );
                    strategy             = new MultipleExportStrategy( contentWriterFactory, contentFactory );
                    break;

                default:
                    throw new ArgumentException( $"Unsupported format :{format}" );
            }

            return strategy;
        }

        public static IExportStrategy Create( Stream targetStream, ExportSupportedFormat format )
        {
            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory = new ExportLeaveOpenedStreamContentWriterFactory( targetStream );
            IExportStrategy strategy;

            switch( format )
            {
                case ExportSupportedFormat.Yaml:
                    contentFactory = new YamlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                case ExportSupportedFormat.Xlsx:
                    contentFactory = new ClosedXmlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( format ), format, null );
            }

            return strategy;
        }
    }
}
