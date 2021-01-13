using System.IO;

namespace KeySwitchManager.Domain.Commons
{
    public class FilePath : IPath
    {
        public string Path { get; }
        public bool Exists => File.Exists( Path );
        public bool IsFile => true;
        public bool IsDirectory => false;

        public FilePath( string path )
        {
            Path = path;
        }

        public override string ToString() => Path;
    }
}