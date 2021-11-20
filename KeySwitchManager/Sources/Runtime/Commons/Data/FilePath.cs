using System.IO;

namespace KeySwitchManager.Commons.Data
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

        public void CreateNew()
        {
            if( !Exists )
            {
                using var _ = File.Create( this.Path );
            }
        }

        public Stream OpenStream( FileMode mode, FileAccess access )
            => File.Open( Path, mode, access );

        public Stream OpenReadStream()
            => File.OpenRead( Path );

        public Stream OpenWriteStream()
            => File.OpenWrite( Path );

        public override string ToString() => Path;
    }
}