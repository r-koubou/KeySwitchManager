using System;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public static class ImportControllerFactory
    {
        public static IController Create( string databasePath, string importFilePath, ILogView logView )
        {
            var path = importFilePath.ToLower();

            if( path.EndsWith( ".xlsx" ) )
            {
                var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
                var spreadSheetFileRepository = new ClosedXmlFileLoadRepository( new FilePath( importFilePath ) );
                var presenter = new ImportSpreadSheetPresenter( logView );
                return new ImportXlsxController( databaseRepository, spreadSheetFileRepository, presenter );
            }

            if( path.EndsWith( ".yaml" ) )
            {
                var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
                var yamlFileRepository = new YamlKeySwitchFileRepository( new FilePath( importFilePath ), true );
                var presenter = new YamlImportPresenter( logView );
                return new ImportYamlController( databaseRepository, yamlFileRepository, presenter );
            }

            throw new ArgumentException( $"{importFilePath} is unknown file format" );
        }
    }
}
