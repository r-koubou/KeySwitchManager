using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class FileExportContentWriter : IExportContentWriter
    {
        private IFilePath OutputPath { get; }

        public FileExportContentWriter( IFilePath outputPath )
        {
            OutputPath = outputPath;
        }

        public async Task WriteAsync( IContent content, CancellationToken cancellationToken = default )
        {
            await using var outputStream = OutputPath.OpenWriteStream();
            await using var contentStream = await content.GetContentStreamAsync();
            await contentStream.CopyToAsync( outputStream, cancellationToken );
        }
    }
}
