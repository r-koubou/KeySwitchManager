using ArticulationManager.Domain.KeySwitches.Value;
using ArticulationManager.Domain.Translations;
using ArticulationManager.Gateways.KeySwitches;
using ArticulationManager.Presenters.KeySwitches;
using ArticulationManager.UseCases.KeySwitches.Exporting.Text;

namespace ArticulationManager.Interactors.KeySwitches.Exporting.Text
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