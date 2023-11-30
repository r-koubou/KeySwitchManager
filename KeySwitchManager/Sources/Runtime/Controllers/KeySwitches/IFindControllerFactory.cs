using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface IFindControllerFactory
    {
        IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView );
    }
}
