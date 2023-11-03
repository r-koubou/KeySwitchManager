using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class AbstractStreamExportContentWriter : IExportContentWriter
    {
        public async Task WriteAsync( IContent content, CancellationToken cancellationToken = default )
        {
            var stream = OpenStream();
            await using var contentStream = await content.GetContentStreamAsync();
            await contentStream.CopyToAsync( stream, cancellationToken );
            DisposeStream( stream );
        }

        protected abstract Stream OpenStream();
        protected abstract void DisposeStream( Stream stream );
    }
}
