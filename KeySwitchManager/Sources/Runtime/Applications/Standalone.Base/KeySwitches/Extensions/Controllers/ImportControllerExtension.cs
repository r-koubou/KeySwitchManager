using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Base.KeySwitches.Helpers;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Applications.Standalone.Base.KeySwitches.Extensions.Controllers
{
    public static class ImportControllerExtension
    {
        #region Import from local file
        public static void Execute(
            this ImportController me,
            string databasePath,
            string importFilePath,
            IImportFilePresenter presenter )
            => ExecuteAsync( me, databasePath, importFilePath,  presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
            this ImportController me,
            string databasePath,
            string importFilePath,
            IImportFilePresenter presenter,
            CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );

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

            await me.ExecuteAsync( repository, content, contentReader, presenter, cancellationToken );
        }
        #endregion
    }
}
