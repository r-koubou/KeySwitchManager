using System;
using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Create
{
    public interface ICreateControllerFactory
    {
        IController Create( string outputFilePath, IOutputPort<CreateOutputData> presenter );
    }

    public class CreateFileControllerFactory : ICreateControllerFactory
    {
        IController ICreateControllerFactory.Create( string outputFilePath, IOutputPort<CreateOutputData> presenter )
        {
            var outputDirectory = new DirectoryPath( Path.GetDirectoryName( outputFilePath ) ?? string.Empty );
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

            return new CreateFileController( strategy, presenter );
        }
    }
}
