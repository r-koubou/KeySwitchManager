using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class StreamExportContentWriter : IExportContentWriter
    {
        public async Task WriteAsync( IContent content, CancellationToken cancellationToken = default )
        {
            await using var stream = OpenStream();
            await using var contentStream = await content.GetContentStreamAsync();
            await contentStream.CopyToAsync( stream, cancellationToken );
        }

        protected abstract Stream OpenStream();
    }
}
