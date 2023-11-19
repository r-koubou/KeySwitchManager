using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IContent
    {
        Stream GetContentStream()
            => GetContentStreamAsync().GetAwaiter().GetResult();

        Task<Stream> GetContentStreamAsync( CancellationToken cancellationToken = default );
    }
}
