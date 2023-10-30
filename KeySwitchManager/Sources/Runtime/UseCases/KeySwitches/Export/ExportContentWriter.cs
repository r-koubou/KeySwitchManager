using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public abstract class ExportContentWriter : IExportContentWriter
    {
        private Stream Stream { get; }

        protected ExportContentWriter( Stream stream )
        {
            Stream = stream;
        }

        public abstract Task WriteAsync( IContent content, CancellationToken cancellationToken = default );
    }
}
