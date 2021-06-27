using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate;
using KeySwitchManager.UseCase.KeySwitches.Create.Template;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Create
{
    public class CreateXlsxController : IController
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ISpreadsheetTemplateExportPresenter Presenter { get; }

        public CreateXlsxController(
            IKeySwitchRepository outputRepository,
            ISpreadsheetTemplateExportPresenter presenter )
        {
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public void Dispose() {}

        public void Execute()
        {
            var interactor = new SpreadsheetTemplateExportInteractor( OutputRepository, Presenter );
            var request = new SpreadsheetTemplateExportRequest();
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }
    }
}
