using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface IDeleteControllerFactory
    {
        IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView );
    }
}
