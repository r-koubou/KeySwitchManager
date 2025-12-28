using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlFileRepository : YamlMemoryRepository
    {
        private FilePath YamlFilePath { get; }
        public YamlFileRepository( FilePath filePath ) : base( filePath.OpenReadStream() )
        {
            YamlFilePath = filePath;
        }

        public override async Task<int> FlushAsync( CancellationToken cancellationToken = default )
        {
            await using var stream = YamlFilePath.OpenWriteStream();
            await WriteBinaryToAsync( stream, cancellationToken );
            return Count();
        }
    }
}
