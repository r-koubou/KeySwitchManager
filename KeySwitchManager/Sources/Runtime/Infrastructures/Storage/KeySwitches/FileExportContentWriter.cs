using System.Threading.Tasks;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.UseCase.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class FileExportContentWriter : IExportContentWriter
    {
        private FilePath OutputPath { get; }

        public FileExportContentWriter( FilePath outputPath )
        {
            OutputPath = outputPath;
        }

        public async Task WriteAsync( IContent content )
        {
            await using var outputStream = OutputPath.OpenWriteStream();
            await using var contentStream = await content.GetContentStreamAsync();
            await contentStream.CopyToAsync( outputStream );
        }
    }
}