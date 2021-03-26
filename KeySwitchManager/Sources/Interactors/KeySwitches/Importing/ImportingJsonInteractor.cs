using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitch;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;
using KeySwitchManager.UseCases.KeySwitches.Translators;

namespace KeySwitchManager.Interactors.KeySwitches.Importing
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
            var insertedCount = 0;
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                var r = Repository.Save( i );
                updatedCount += r.Updated;
                insertedCount += r.Inserted;
            }

            Presenter.Present( $"{insertedCount} record(s) inserted, {updatedCount} record(s) updated" );

            return new ImportingTextResponse( updatedCount );
        }
    }
}