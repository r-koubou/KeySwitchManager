using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Import
{
    public class FileContent : IContent
    {
        private IFilePath FilePath { get; }

        public FileContent( IFilePath filePath )
        {
            FilePath = filePath;
        }

        public Task<Stream> GetContentStreamAsync( CancellationToken cancellationToken = default )
        {
            return Task.FromResult( FilePath.OpenReadStream() );
        }
    }
}
