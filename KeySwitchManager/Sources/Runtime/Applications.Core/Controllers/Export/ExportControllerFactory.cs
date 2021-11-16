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

            IController CreateDawController( IKeySwitchRepository targetFileRepository )
            {
                return new ExportDawController( developer, product, instrument, sourceDatabase, targetFileRepository, new ExportDawPresenter( logTextView ) );
            }

            try
            {
                var subject = new Subject<string>();
                subject.Subscribe( new LoggingObserver( logTextView ) );

                var observer = new Subject<string>().AsObserver();

                switch( format )
                {
                    case ExportSupportedFormat.Yaml:
                        var yamlWriter = new MultipleYamlFileWriter( outputDir );
                        return new ExportFileController( developer, product, instrument, sourceDatabase, yamlWriter, new ExportFilePresenter( logTextView ), observer );

                    case ExportSupportedFormat.Xlsx:
                        var xlsxRepository = new ClosedXmlFileSaveRepository( outputDir );
                        xlsxRepository.LoggingObservable.Subscribe( new LoggingObserver( logTextView ) );
                        return new ExportXlsxController( developer, product, sourceDatabase, xlsxRepository, new ExportXlsxPresenter( logTextView ) );

                    case ExportSupportedFormat.Cubase:
                        var cubaseRepository = new CubaseFileRepository( outputDir );
                        cubaseRepository.LoggingObservable.Subscribe( new LoggingObserver( logTextView ) );
                        return CreateDawController( cubaseRepository );

                    case ExportSupportedFormat.StudioOne:
                        var studioOneRepository = new StudioOneFileRepository( outputDir );
                        studioOneRepository.LoggingObservable.Subscribe( new LoggingObserver( logTextView ) );
                        return CreateDawController( studioOneRepository );

                    case ExportSupportedFormat.Cakewalk:
                        var cakewalkRepository = new CakewalkFileRepository( outputDir );
                        cakewalkRepository.LoggingObservable.Subscribe( new LoggingObserver( logTextView ) );
                        return CreateDawController( cakewalkRepository );
                    default:
                        throw new ArgumentException( $"Unsupported format :{format}" );
                }
            }
            catch
            {
                Disposer.Dispose( sourceDatabase );
                throw;
            }

            Disposer.Dispose( sourceDatabase );
            throw new ArgumentException( $"{format} is not supported" );
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
