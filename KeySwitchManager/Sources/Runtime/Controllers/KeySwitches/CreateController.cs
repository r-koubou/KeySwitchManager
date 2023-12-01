using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class CreateController
    {
        public async Task ExecuteAsync( IExportStrategy strategy, ICreatePresenter presenter, CancellationToken cancellationToken )
        {
            var interactor = new CreateInteractor( presenter );
            var inputData = new CreateInputData( strategy );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
