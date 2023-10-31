using System.IO;

using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public static class ImportControllerFactory
    {
        public static IController Create( string databasePath, string importFilePath, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new ImportFilePresenter( logTextView );

            IImportContentReader contentReader;
            var fileExtension = Path.GetExtension( importFilePath ).ToLower();

            switch( Path.GetExtension( importFilePath ).ToLower() )
            {
                case ".yaml":
                case ".yml":
                    contentReader = new YamlImportContentReader();
                    break;
                case ".xlsx":
                    contentReader = new ClosedXmlImportContentReader();
                    break;
                default:
                    throw new InvalidDataException( $"Unknown file extension: {fileExtension}" );
            }

            var content = new FileContent( new FilePath( importFilePath ) );

            return new ImportFileController( databaseRepository, contentReader, content, presenter );
        }
    }
}
