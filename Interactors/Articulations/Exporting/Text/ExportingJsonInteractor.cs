using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Interactors.Articulations.Exporting.Text
{
    public class ExportingJsonInteractor : IExportingTextUseCase
    {
        private IArticulationRepository Repository { get; }
        private IEntityTranslationService TranslationService { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingJsonInteractor(
            IArticulationRepository repository,
            IExportingTextPresenter presenter,
            IEntityTranslationService translationService )
        {
            Repository = repository;
            Presenter  = presenter;
            TranslationService = translationService;
        }

        public void Execute( InputData inputData )
        {
            var developerName = new DeveloperName( inputData.DeveloperName );
            var productName = new ProductName( inputData.ProductName );

            var entities = Repository.Find( developerName, productName );

            Presenter.Output( new OutputData( TranslationService.Translate( entities ) ) );
        }
    }
}