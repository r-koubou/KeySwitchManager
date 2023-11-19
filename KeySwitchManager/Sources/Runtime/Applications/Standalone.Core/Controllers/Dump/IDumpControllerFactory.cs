using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Dump
{
    public interface IDumpControllerFactory
    {
        IController Create( string databasePath, string outputFilePath, ILogTextView logTextView );
    }
}
