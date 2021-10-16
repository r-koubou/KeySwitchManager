using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.Text;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateYamlController : IController
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ICreateTextTemplatePresenter Presenter { get; }

        public CreateYamlController(
            IKeySwitchRepository outputRepository,
            ICreateTextTemplatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public void Dispose() {}

        public void Execute()
        {
            var interactor = new CreateTextTemplateInteractor( OutputRepository, Presenter );
            var response = interactor.Execute();
            Presenter.Complete( response );
        }
    }
}
