using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Dump
{
    public interface IDumpControllerFactory
    {
        IController Create( string databasePath, string outputFilePath, ILogTextView logTextView );
    }
}
