using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Create
{
    public sealed class CreateController : IController
    {
        private IExportStrategy Strategy { get; }
        private ICreatePresenter Presenter { get; }

        public CreateController(
            IExportStrategy strategy,
            ICreatePresenter presenter )
        {
            Strategy  = strategy;
            Presenter = presenter;
        }

        public void Dispose() {}

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            ICreateUseCase interactor = new CreateInteractor( Presenter );
            var request = new CreateInputData( Strategy );
            await interactor.HandleAsync( request, cancellationToken );
        }

        #region Factory
        public static CreateController Create( string outputFilePath, ICreatePresenter presenter )
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

            return new CreateController( strategy, presenter );
        }

        public static CreateController Create( Stream targetStream, ExportSupportedFormat format, ICreatePresenter presenter )
        {
            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory = new ExportLeaveOpenedStreamContentWriterFactory( targetStream );
            IExportStrategy strategy;

            switch( format )
            {
                case ExportSupportedFormat.Yaml:
                    contentFactory = new YamlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                case ExportSupportedFormat.Xlsx:
                    contentFactory = new ClosedXmlExportContentFactory();
                    strategy       = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( format ), format, null );
            }

            return new CreateController( strategy, presenter );
        }
        #endregion
    }
}
