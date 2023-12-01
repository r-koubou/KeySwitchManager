using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers
{
    public sealed class CreateController
    {
        #region To File
        public void Execute( string outputFilePath, ICreatePresenter presenter )
            => ExecuteAsync( outputFilePath, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync( string outputFilePath, ICreatePresenter presenter, CancellationToken cancellationToken )
        {
            var strategy = CreateStrategy( outputFilePath );

            await ExecuteImplAsync( strategy, presenter, cancellationToken );
        }
        #endregion

        #region To Stream
        public void Execute( Stream targetStream, ICreatePresenter presenter, ExportFormat format )
            => ExecuteAsync( targetStream, presenter, format ).GetAwaiter().GetResult();

        public async Task ExecuteAsync( Stream targetStream, ICreatePresenter presenter, ExportFormat format, CancellationToken cancellationToken = default )
        {
            var strategy = CreateStrategy( targetStream, format );

            await ExecuteImplAsync( strategy, presenter, cancellationToken );
        }
        #endregion

        private static async Task ExecuteImplAsync( IExportStrategy strategy, ICreatePresenter presenter, CancellationToken cancellationToken )
        {
            var interactor = new CreateInteractor( presenter );
            var inputData = new CreateInputData( strategy );

            await interactor.HandleAsync( inputData, cancellationToken );
        }

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
