using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Export
{
    public interface IExportControllerFactory
    {
        public IController Create(
            string developerName,
            string productName,
            string instrumentName,
            ExportSupportedFormat format,
            ILogTextView logTextView );
    }
}
