using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Export
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
