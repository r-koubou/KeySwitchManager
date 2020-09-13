using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Interactors.Articulations.Exporting.Text
{
    public class ExportingJsonInteractor : IExportingTextUseCase
    {
        private IArticulationRepository Repository { get; }
        private IArticulationToText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingJsonInteractor(
            IArticulationRepository repository,
            IExportingTextPresenter presenter,
            IArticulationToText translator )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        public void Execute( InputData inputData )
        {
            var developerName = new DeveloperName( inputData.DeveloperName );
            var productName = new ProductName( inputData.ProductName );

            var entities = Repository.Find( developerName, productName );

            Presenter.Output( new OutputData( Translator.Translate( entities ) ) );
        }
    }
}