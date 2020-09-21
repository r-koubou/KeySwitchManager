using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx;
using KeySwitchManager.UseCases.KeySwitches.Translations;

namespace KeySwitchManager.Interactors.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxInteractor : IImportingXlsxUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IXlsxWorkbookToKeySwitchList Translator { get; }
        private IImportingXlsxPresenter Presenter { get; }

        public ImportingXlsxInteractor(
            IKeySwitchRepository repository,
            IXlsxWorkbookToKeySwitchList translator,
            IImportingXlsxPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
            Translator = translator;
        }

        public ImportingXlsxResponse Execute( ImportingXlsxRequest request )
        {
            var keySwitches = Translator.Translate( request.FilePath );
            var updatedCount = 0;

            foreach( var i in keySwitches )
            {
                updatedCount += Repository.Save( i );
            }

            Presenter.Present( $"{updatedCount} record(s) updated" );

            return new ImportingXlsxResponse( updatedCount );
        }
    }
}