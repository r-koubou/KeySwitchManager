using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportFilePresenter : IOutputPort<ImportOutputData>
    {
        void HandleImported( KeySwitch keySwitch )
            => HandleImportedAsync( keySwitch ).GetAwaiter().GetResult();

        Task HandleImportedAsync( KeySwitch keySwitch, CancellationToken cancellationToken = default );
    }
}
