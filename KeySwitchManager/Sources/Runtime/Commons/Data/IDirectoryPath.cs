namespace KeySwitchManager.Commons.Data
{
    public interface IDirectoryPath : IPath
    {
        bool IPath.IsFile => false;
        bool IPath.IsDirectory => true;
    }
}
