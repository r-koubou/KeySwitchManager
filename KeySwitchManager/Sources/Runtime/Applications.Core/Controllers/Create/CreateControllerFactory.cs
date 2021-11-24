using System;
using System.IO;

using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public static class CreateControllerFactory
    {
        public static IController Create( string outputFilePath, ILogTextView logTextView )
        {
            var path = outputFilePath.ToLower();

            if( path.EndsWith( ".xlsx" ) )
            {
                return CreateImpl( outputFilePath, logTextView, ( stream ) => new ClosedXmlWriter( stream ) );
            }

            if( path.EndsWith( ".yaml" ) || path.EndsWith( ".yml" ) )
            {
                return CreateImpl( outputFilePath, logTextView, ( stream ) => new YamlKeySwitchWriter( stream ) );
            }

            if( path.EndsWith( ".db" ) )
            {
                return CreateImpl( logTextView, new LiteDbFileWriter( new FilePath( outputFilePath ) ) );
            }

            throw new ArgumentException( $"{outputFilePath} is unknown file format" );
        }

        private static IController CreateImpl( string outputFilePath, ILogTextView logTextView, Func<Stream, IKeySwitchWriter> factory )
        {
            var stream = new FilePath( outputFilePath ).OpenWriteStream();
            var writer = factory.Invoke( stream );
            var presenter = new CreateFilePresenter( logTextView );
            return new CreateFileController( writer, presenter );
        }

        private static IController CreateImpl( ILogTextView logTextView, IKeySwitchWriter writer )
        {
            var presenter = new CreateFilePresenter( logTextView );
            return new CreateFileController( writer, presenter );
        }

    }
}
