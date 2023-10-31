namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public interface IImportControllerFactory
    {
        IController Create( string databasePath, string importFilePath );
    }
}
