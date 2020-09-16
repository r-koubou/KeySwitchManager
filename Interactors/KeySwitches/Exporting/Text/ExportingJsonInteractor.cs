using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Exporting.Text;

namespace KeySwitchManager.Interactors.KeySwitches.Exporting.Text
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

        public ExportingTextResponse Execute( ExportingTextRequest request )
        {
            var developerName = new DeveloperName( request.DeveloperName );
            var productName = new ProductName( request.ProductName );

            var entities = Repository.Find( developerName, productName );

            foreach( var i in entities )
            {
                Presenter.Present( Translator.Translate( i ).Value );
            }

            return new ExportingTextResponse();
        }
    }
}