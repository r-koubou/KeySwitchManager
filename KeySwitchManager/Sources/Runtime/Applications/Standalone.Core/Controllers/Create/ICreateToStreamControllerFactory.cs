using System.IO;

using KeySwitchManager.Applications.Standalone.Core.Controllers.Export;
using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Create
{
    public interface ICreateToStreamControllerFactory
    {
        IController Create( Stream targetStream, ExportSupportedFormat format, ILogTextView logTextView );
    }
}
