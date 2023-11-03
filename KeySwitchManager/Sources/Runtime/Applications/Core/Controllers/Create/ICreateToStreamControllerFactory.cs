using System.IO;

using KeySwitchManager.Applications.Core.Controllers.Export;
using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public interface ICreateToStreamControllerFactory
    {
        IController Create( Stream targetStream, ExportSupportedFormat format, ILogTextView logTextView );
    }
}
