using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportContentWriter
    {
        Task WriteAsync( IContent content, CancellationToken cancellationToken = default );
    }
}
