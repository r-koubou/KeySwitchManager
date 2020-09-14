using ArticulationManager.Domain.Translations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Interactors.Articulations.Importing.Text
{
    public class ImportingJsonInteractor : IExportingTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private ITextToKeySwitch Translator { get; }
        private IImportingTextPresenter Presenter { get; }

        public ImportingJsonInteractor(
            IKeySwitchRepository repository,
            IImportingTextPresenter presenter,
            ITextToKeySwitch translator )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        public void Execute( InputData inputData )
        {
        }
    }
}