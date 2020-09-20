using System.IO;

namespace KeySwitchManager.Domain.Commons
{
    public class DirectoryPath : IPath
    {
        public string Path { get; }
        public bool Exists => Directory.Exists( Path );

        public DirectoryPath( string path )
        {
            Path   = path;
        }
    }
}