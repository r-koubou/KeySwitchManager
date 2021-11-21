using System;
using System.Reactive;
using System.Reactive.Subjects;

using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Json.Cakewalk;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Xml.Cubase;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public static class ExportControllerFactory
    {
        public static IController Create(
            string developerName,
            string productName,
            string instrumentName,
            string databaseFile,
            string outputDirectory,
            ExportSupportedFormat format,
            ILogTextView logTextView )
        {
            var developer = new DeveloperName( developerName );
            var product = new ProductName( productName );
            var instrument = new InstrumentName( instrumentName );
            var databasePath = new FilePath( databaseFile );
            var outputDir = new DirectoryPath( outputDirectory );

            var sourceDatabase = new LiteDbKeySwitchRepository( databasePath );

            try
            {
                var subject = new Subject<string>();
                subject.Subscribe( new LoggingObserver( logTextView ) );

                var observer = subject.AsObserver();

                IController CreateImpl( IKeySwitchWriter writer )
                    => new ExportFileController( developer, product, instrument, sourceDatabase, writer, new ExportFilePresenter( logTextView ), observer );

                switch( format )
                {
                    case ExportSupportedFormat.Yaml:
                        return CreateImpl( new MultipleYamlFileWriter( outputDir ) );

                    case ExportSupportedFormat.Xlsx:
                        return CreateImpl( new MultipleClosedXmlWriter( outputDir ) );

                    case ExportSupportedFormat.Cubase:
                        return CreateImpl( new MultipleCubaseWriter( outputDir ) );

                    case ExportSupportedFormat.StudioOne:
                        return CreateImpl( new MultipleStudioOneWriter( outputDir ) );

                    case ExportSupportedFormat.Cakewalk:
                        return CreateImpl( new MultipleCakewalkWriter( outputDir ) );
                    default:
                        Disposer.Dispose( sourceDatabase );
                        throw new ArgumentException( $"Unsupported format :{format}" );
                }
            }
            catch
            {
                Disposer.Dispose( sourceDatabase );
                throw;
            }
        }

        private class LoggingObserver : IObserver<string>
        {
            private ILogTextView LogTextView { get; }

            public LoggingObserver( ILogTextView logTextView )
            {
                LogTextView = logTextView;
            }

            public void OnCompleted() {}

            public void OnError( Exception error )
            {
                LogTextView.Append( error.ToString() );
            }

            public void OnNext( string value )
            {
                LogTextView.Append( value );
            }
        }

    }
}
