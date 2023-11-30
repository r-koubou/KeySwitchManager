using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches.Dump
{
    public interface IDumpControllerFactory
    {
        IController Create( string databasePath, string outputFilePath, ILogTextView logTextView );
    }
}
