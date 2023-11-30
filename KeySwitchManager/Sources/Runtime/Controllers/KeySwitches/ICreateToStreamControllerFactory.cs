using System.IO;

using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches.Create
{
    public interface ICreateToStreamControllerFactory
    {
        IController Create( Stream targetStream, ExportSupportedFormat format, ILogTextView logTextView );
    }
}
