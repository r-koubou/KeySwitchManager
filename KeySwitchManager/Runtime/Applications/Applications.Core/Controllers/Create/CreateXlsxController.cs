using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet;

namespace Application.Core.Controllers.Create
{
    public class CreateXlsxController : IController
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ICreateSpreadsheetTemplatePresenter Presenter { get; }

        public CreateXlsxController(
            IKeySwitchRepository outputRepository,
            ICreateSpreadsheetTemplatePresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public void Dispose() {}

        public void Execute()
        {
            var interactor = new CreateSpreadsheetTemplateInteractor( OutputRepository, Presenter );
            var request = new CreateSpreadsheetTemplateRequest();
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }
    }
}
