using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.KeySwitches.Controllers.Extensions
{
    public static class CreateControllerExtension
    {
        #region To File
        public static void CreateToLocalFile( this CreateController me, string outputFilePath, ICreatePresenter presenter )
            => CreateToLocalFileAsync( me, outputFilePath, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public static async Task CreateToLocalFileAsync( this CreateController me, string outputFilePath, ICreatePresenter presenter, CancellationToken cancellationToken )
        {
            var strategy = CreateStrategy( outputFilePath );

            await me.CreateAsync( strategy, presenter, cancellationToken );
        }
        #endregion

        #region To Stream
        public static void CreateStream( this CreateController me, Stream targetStream, ICreatePresenter presenter, ExportFormat format )
            => CreateToStreamAsync( me, targetStream, presenter, format ).GetAwaiter().GetResult();

        public static async Task CreateToStreamAsync( this CreateController me, Stream targetStream, ICreatePresenter presenter, ExportFormat format, CancellationToken cancellationToken = default )
        {
            var strategy = CreateStrategy( targetStream, format );

            await me.CreateAsync( strategy, presenter, cancellationToken );
        }
        #endregion

        #region Strategy Factory
        private static IExportStrategy CreateStrategy( string outputFilePath )
        {
            var fileName = outputFilePath.ToLower();

            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory;
            IExportStrategy strategy;

            if( fileName.EndsWith( ".xlsx" ) )
            {
                contentFactory       = new ClosedXmlExportContentFactory();
                contentWriterFactory = new ClosedXmlExportContentFileWriterFactory( new SpecificExportPathBuilder( new FilePath( outputFilePath ) ) );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else if( fileName.EndsWith( ".yaml" ) || fileName.EndsWith( ".yml" ) )
            {
                contentFactory       = new YamlExportContentFactory();
                contentWriterFactory = new YamlExportContentFileWriterFactory( new SpecificExportPathBuilder( new FilePath( outputFilePath ) ) );
                strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
            }
            else
            {
                throw new ArgumentException( $"{outputFilePath} is unknown file format" );
            }

            return strategy;
        }

        private static IExportStrategy CreateStrategy( Stream targetStream, ExportFormat format )
        {
            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory = new ExportLeaveOpenedStreamContentWriterFactory( targetStream );
            IExportStrategy strategy;

            switch( format )
            {
                case ExportFormat.Yaml:
                    contentFactory = new YamlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                case ExportFormat.Xlsx:
                    contentFactory = new ClosedXmlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( format ), format, null );
            }

            return strategy;
        }
        #endregion
    }
}
