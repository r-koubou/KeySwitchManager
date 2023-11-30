using System;
using System.IO;

using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.Presenters;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Create
{
    public class CreateToStreamControllerFactory : ICreateToStreamControllerFactory
    {
        public IController Create( Stream targetStream, ExportSupportedFormat format, ILogTextView logTextView )
        {
            IExportContentFactory contentFactory;
            IExportContentWriterFactory contentWriterFactory = new ExportLeaveOpenedStreamContentWriterFactory( targetStream );
            IExportStrategy strategy;

            switch( format )
            {
                case ExportSupportedFormat.Yaml:
                    contentFactory       = new YamlExportContentFactory();
                    strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                case ExportSupportedFormat.Xlsx:
                    contentFactory       = new ClosedXmlExportContentFactory();
                    strategy             = new SingleExportStrategy( contentWriterFactory, contentFactory );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( nameof( format ), format, null );
            }

            return new CreateFileController( strategy, CreatePresenter.Null );
        }
    }
}
