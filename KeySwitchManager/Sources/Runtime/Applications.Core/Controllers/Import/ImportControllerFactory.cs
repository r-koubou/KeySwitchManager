using System;

using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public static class ImportControllerFactory
    {
        public static IController Create( string databasePath, string importFilePath, ILogTextView logTextView )
        {
            var databaseRepository = new LiteDbRepository( new FilePath( databasePath ) );
            databaseRepository.LoggingObservable.Subscribe( new DatabaseAccessObserver( logTextView ) );

            var reader = KeySwitchFileReaderFactory.Create( importFilePath );
            var presenter = new ImportFilePresenter( logTextView );

            return new ImportFileController( databaseRepository, reader, presenter );
        }

        private class DatabaseAccessObserver : IObserver<string>
        {
            private ILogTextView LogTextView { get; }

            public DatabaseAccessObserver( ILogTextView logTextView )
            {
                LogTextView = logTextView;
            }

            public void OnCompleted() {}

            public void OnError( Exception error ) {}

            public void OnNext( string value )
            {
                LogTextView.Append( value );
            }
        }
    }
}
