using System.IO;

namespace KeySwitchManager.Commons.Data
{
    public class DirectoryPath : IDirectoryPath
    {
        public static DirectoryPath Default { get; } = new DirectoryPath( "." );

        public string Path { get; }
        public bool Exists => Directory.Exists( Path );

        public DirectoryPath( string path )
        {
            Path = path;
        }

        public void CreateNew()
        {
            if( !Exists )
            {
                Directory.CreateDirectory( Path );
            }
        }

        public override string ToString() => Path;

    }
}
