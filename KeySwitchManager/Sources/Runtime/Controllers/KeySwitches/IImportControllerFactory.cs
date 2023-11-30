namespace KeySwitchManager.Controllers.KeySwitches.Import
{
    public interface IImportControllerFactory
    {
        IController Create( string databasePath, string importFilePath );
    }
}
