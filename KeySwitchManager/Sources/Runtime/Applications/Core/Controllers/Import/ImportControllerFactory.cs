using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public static class ImportControllerFactory
    {
        public static IController Create( string databasePath, string importFilePath, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var reader = KeySwitchFileReaderFactory.Create( importFilePath );
            var presenter = new ImportFilePresenter( logTextView );

            return new ImportFileController( databaseRepository, reader, presenter );
        }
    }
}
