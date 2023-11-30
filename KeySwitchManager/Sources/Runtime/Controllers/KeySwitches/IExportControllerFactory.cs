using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches.Export
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
