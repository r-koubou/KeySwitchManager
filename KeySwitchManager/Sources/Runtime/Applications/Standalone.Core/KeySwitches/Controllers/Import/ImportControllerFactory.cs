using System.IO;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Import
{
    public class ImportControllerFactory : IImportControllerFactory
    {
        private ILogTextView LogTextView { get; }

        public ImportControllerFactory( ILogTextView logTextView )
        {
            LogTextView = logTextView;
        }

        public IController Create( string databasePath, string importFilePath )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new ImportPresenter( LogTextView );

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

            return new ImportController( databaseRepository, contentReader, content, presenter );
        }
    }
}
