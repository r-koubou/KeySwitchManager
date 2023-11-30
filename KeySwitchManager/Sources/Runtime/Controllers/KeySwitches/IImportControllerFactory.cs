namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface IImportControllerFactory
    {
        IController Create( string databasePath, string importFilePath );
    }
}
