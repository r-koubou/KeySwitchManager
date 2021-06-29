using System;

using KeySwitchManager.AppCore.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.AppCore.Controllers.Create
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
                var yamlFileRepository = new YamlKeySwitchFileRepository( new FilePath( outputFilePath ), false );
                var presenter = new CreateYamlKeySwitchPresenter( logTextView );
                return new CreateYamlController( yamlFileRepository, presenter );
            }

            throw new ArgumentException( $"{outputFilePath} is unknown file format" );
        }
    }
}
