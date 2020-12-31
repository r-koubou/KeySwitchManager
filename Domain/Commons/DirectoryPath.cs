using System.IO;

namespace KeySwitchManager.Domain.Commons
{
    public class DirectoryPath : IPath
    {
        public string Path { get; }
        public bool Exists => Directory.Exists( Path );
        public bool IsFile => false;
        public bool IsDirectory => true;

        public DirectoryPath( string path )
        {
            Path   = path;
        }

        public override string ToString() => Path;

    }
}