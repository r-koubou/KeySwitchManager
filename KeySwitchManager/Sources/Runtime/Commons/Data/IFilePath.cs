using System.IO;

namespace KeySwitchManager.Commons.Data
{
    public interface IFilePath : IPath
    {
        bool IPath.IsFile => true;
        bool IPath.IsDirectory => true;

        Stream OpenStream( FileMode mode, FileAccess access );
        Stream OpenReadStream();
        Stream OpenWriteStream(bool overWrite = true);
        Stream OpenReadWriteStream(bool overWrite = true);
    }
}
