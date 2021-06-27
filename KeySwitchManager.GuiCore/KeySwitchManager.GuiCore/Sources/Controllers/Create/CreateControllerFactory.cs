using System;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public static class CreateControllerFactory
    {
        public static IController Create( string outputFilePath, ILogView logView )
        {
            var path = outputFilePath.ToLower();

            if( path.EndsWith( ".xlsx" ) )
            {
                var xlsxFileRepository = new ClosedXmlFileSaveTemplateRepository( new FilePath( outputFilePath ) );
                var presenter = new CreateXlsxKeySwitchPresenter( logView );
                return new CreateXlsxController( xlsxFileRepository, presenter );
            }

            if( path.EndsWith( ".yaml" ) )
            {
                var yamlFileRepository = new YamlKeySwitchFileRepository( new FilePath( outputFilePath ), false );
                var presenter = new CreateYamlKeySwitchPresenter( logView );
                return new CreateYamlController( yamlFileRepository, presenter );
            }

            throw new ArgumentException( $"{outputFilePath} is unknown file format" );
        }
    }
}
