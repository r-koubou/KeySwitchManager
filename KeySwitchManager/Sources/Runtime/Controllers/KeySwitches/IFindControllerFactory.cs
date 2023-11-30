using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches.Find
{
    public interface IFindControllerFactory
    {
        IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView );
    }
}
