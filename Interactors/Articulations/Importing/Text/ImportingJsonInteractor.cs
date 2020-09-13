using ArticulationManager.Domain.Translations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Interactors.Articulations.Importing.Text
{
    public class ImportingJsonInteractor : IExportingTextUseCase
    {
        private IArticulationRepository Repository { get; }
        private ITextToArticulation Translator { get; }
        private IImportingTextPresenter Presenter { get; }

        public ImportingJsonInteractor(
            IArticulationRepository repository,
            IImportingTextPresenter presenter,
            ITextToArticulation translator )
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