using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface IDumpControllerFactory
    {
        IController Create( string databasePath, string outputFilePath, ILogTextView logTextView );
    }
}
