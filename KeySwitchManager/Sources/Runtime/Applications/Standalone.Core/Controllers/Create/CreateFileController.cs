using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Create
{
    public class CreateFileController : IController
    {
        private IExportStrategy Strategy { get; }
        private ICreatePresenter Presenter { get; }

        public CreateFileController(
            IExportStrategy strategy,
            ICreatePresenter presenter )
        {
            Strategy  = strategy;
            Presenter = presenter;
        }

        public void Dispose() {}

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            ICreateUseCase interactor = new CreateInteractor( Strategy, Presenter );
            var response = await interactor.ExecuteAsync( cancellationToken );
            Presenter.Complete( response );
        }
    }
}
