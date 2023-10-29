using System;
using System.IO;

using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public interface ICreateControllerFactory
    {
        IController Create( string outputFilePath, ILogTextView logTextView );
    }

    public class CreateFileControllerFactory : ICreateControllerFactory
    {
        IController ICreateControllerFactory.Create( string outputFilePath, ILogTextView logTextView )
        {
            var outputDirectory = new DirectoryPath( Path.GetDirectoryName( outputFilePath ) ?? string.Empty );
            var path = outputFilePath.ToLower();

            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory;
            IExportStrategy strategy;

            if( path.EndsWith( ".xlsx" ) )
            {
                contentFactory       = new ClosedXmlExportContentFactory();
                contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( outputDirectory );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else if( path.EndsWith( ".yaml" ) || path.EndsWith( ".yml" ) )
            {
                contentFactory       = new YamlExportContentFactory();
                contentWriterFactory = new YamlExportContentFileWriterFactory( outputDirectory );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else
            {
                throw new ArgumentException( $"{outputFilePath} is unknown file format" );
            }

            return new CreateFileController( strategy, new CreateFilePresenter( logTextView ) );
        }
    }
}
