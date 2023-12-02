using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class CreateController
    {
        public void Create( IExportStrategy strategy, ICreatePresenter presenter )
            => CreateAsync( strategy, presenter ).GetAwaiter().GetResult();

        public async Task CreateAsync( IExportStrategy strategy, ICreatePresenter presenter, CancellationToken cancellationToken = default )
        {
            var interactor = new CreateInteractor( presenter );
            var inputData = new CreateInputData( strategy );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
