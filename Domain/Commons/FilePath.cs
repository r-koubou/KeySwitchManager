using System.IO;

namespace KeySwitchManager.Domain.Commons
{
    public class FilePath : IPath
    {
        public string Path { get; }
        public bool Exists => File.Exists( Path );

        public FilePath( string path )
        {
            Path = path;
        }
    }
}