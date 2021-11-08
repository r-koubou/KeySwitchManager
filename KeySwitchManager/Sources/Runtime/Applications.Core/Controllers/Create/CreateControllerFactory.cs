using System;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public static class CreateControllerFactory
    {
        public static IController Create( string outputFilePath, ILogTextView logTextView )
        {
            var path = outputFilePath.ToLower();

            if( path.EndsWith( ".xlsx" ) )
            {
                var xlsxFileRepository = new ClosedXmlFileSaveTemplateRepository( new FilePath( outputFilePath ) );
                var presenter = new CreateXlsxKeySwitchPresenter( logTextView );
                return new CreateXlsxController( xlsxFileRepository, presenter );
            }

            if( path.EndsWith( ".yaml" ) )
            {
                var stream = new FilePath( outputFilePath ).OpenWriteStream();
                var writer = new YamlKeySwitchWriter( stream );
                var presenter = new CreateYamlKeySwitchPresenter( logTextView );
                return new CreateYamlController( writer, presenter );
            }

            throw new ArgumentException( $"{outputFilePath} is unknown file format" );
        }
    }
}
