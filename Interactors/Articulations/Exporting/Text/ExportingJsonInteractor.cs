using ArticulationManager.Domain.Articulations.Value;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Gateways.Articulations;
using ArticulationManager.Presenters.Articulations;
using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Interactors.Articulations.Exporting.Text
{
    public class ExportingJsonInteractor : IExportingTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchToText Translator { get; }
        private IExportingTextPresenter Presenter { get; }

        public ExportingJsonInteractor(
            IKeySwitchRepository repository,
            IExportingTextPresenter presenter,
            IKeySwitchToText translator )
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

            foreach( var i in entities )
            {
                Presenter.Output( new OutputData( Translator.Translate( i ) ) );
            }
        }
    }
}