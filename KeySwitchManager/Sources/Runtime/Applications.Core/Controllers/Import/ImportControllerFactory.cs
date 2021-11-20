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
            var path = importFilePath.ToLower();

            var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
            databaseRepository.LoggingObservable.Subscribe( new DatabaseAccessObserver( logTextView ) );

            var reader = KeySwitchFileReaderFactory.Create( importFilePath );
            var presenter = new ImportFilePresenter( logTextView );

            return new ImportFileController( databaseRepository, reader, presenter );

            // TODO 後で削除する
#if false
            if( path.EndsWith( ".xlsx" ) )
            {
                var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
                databaseRepository.LoggingObservable.Subscribe( new DatabaseAccessObserver( logTextView ) );

                var spreadSheetFileRepository = new ClosedXmlFileLoadRepository( new FilePath( importFilePath ) );
                var presenter = new ImportXlsxPresenter( logTextView );
                return new ImportXlsxController( databaseRepository, spreadSheetFileRepository, presenter );
            }

            if( path.EndsWith( ".yaml" ) )
            {
                var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
                databaseRepository.LoggingObservable.Subscribe( new DatabaseAccessObserver( logTextView ) );

                var reader = new YamlKeySwitchReader( File.OpenRead( importFilePath ) );
                var presenter = new ImportFilePresenter( logTextView );
                return new ImportFileController( databaseRepository, reader, presenter );
            }

            throw new ArgumentException( $"{importFilePath} is unknown file format" );
#endif
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
