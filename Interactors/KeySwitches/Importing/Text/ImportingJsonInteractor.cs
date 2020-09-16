using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Gateways.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.Interactors.KeySwitches.Importing.Text
{
    public class ImportingJsonInteractor : IImportingTextUseCase
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
            var keySwitch = Translator.Translate( inputData.JsonText );
            Repository.Save( keySwitch );
            Presenter.Output( new OutputData() );
        }
    }
}