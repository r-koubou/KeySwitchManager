using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.Template;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public class CreateYamlController : IController
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ITemplateKeySwitchCreatePresenter Presenter { get; }

        public CreateYamlController(
            IKeySwitchRepository outputRepository,
            ITemplateKeySwitchCreatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public void Dispose() {}

        public void Execute()
        {
            var interactor = new TemplateKeySwitchCreateInteractor( OutputRepository, Presenter );
            var response = interactor.Execute();
            Presenter.Complete( response );
        }
    }
}
