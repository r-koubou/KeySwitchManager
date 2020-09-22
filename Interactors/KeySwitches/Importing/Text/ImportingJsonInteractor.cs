using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Interactors.KeySwitches.Importing.Text
{
    public class ImportingJsonInteractor : IImportingTextUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IJsonListToKeySwitchList Translator { get; }
        private IImportingTextPresenter Presenter { get; }

        public ImportingJsonInteractor(
            IKeySwitchRepository repository,
            IJsonListToKeySwitchList translator,
            IImportingTextPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        public ImportingTextResponse Execute( ImportingTextRequest request )
        {
            var keySwitches = Translator.Translate( request.JsonText );
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                updatedCount += Repository.Save( i );
            }

            Presenter.Present( $"{updatedCount} record(s) updated" );

            return new ImportingTextResponse( updatedCount );
        }
    }
}