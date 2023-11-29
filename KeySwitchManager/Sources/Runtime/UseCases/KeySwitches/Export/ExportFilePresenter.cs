using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportPresenter : IOutputPort<ExportOutputData>
    {
        void HandleExported( KeySwitch exported )
            => HandleExportedAsync( exported ).GetAwaiter().GetResult();

        Task HandleExportedAsync( KeySwitch exported, CancellationToken cancellationToken = default );
    }
}
