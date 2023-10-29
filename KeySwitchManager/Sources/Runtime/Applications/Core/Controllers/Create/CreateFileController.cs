using System.Threading.Tasks;

using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateFileController : IController
    {
        private IExportStrategy Strategy { get; }
        private ICreateFilePresenter Presenter { get; }

        public CreateFileController(
            IExportStrategy strategy,
            ICreateFilePresenter presenter )
        {
            Strategy  = strategy;
            Presenter = presenter;
        }

        public void Dispose() {}

        async Task IController.ExecuteAsync()
        {
            ICreateFileUseCase interactor = new CreateFileInteractor( Strategy, Presenter );
            var response = await interactor.ExecuteAsync();
            Presenter.Complete( response );
        }
    }
}
