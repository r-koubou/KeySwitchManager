using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.GuiCore.Sources.Controllers.Import
{
    public class ImportYamlController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private YamlKeySwitchFileRepository YamlFileRepository { get; }
        private IImportTextPresenter Presenter { get; }

        #region Ctor
        public ImportYamlController(
            IKeySwitchRepository databaseRepository,
            YamlKeySwitchFileRepository yamlFileRepository,
            IImportTextPresenter presenter )
        {
            DatabaseRepository = databaseRepository;
            YamlFileRepository = yamlFileRepository;
            Presenter          = presenter;
        }
        #endregion

        public void Dispose()
        {
            try
            {
                DatabaseRepository.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        public void Execute()
        {
            var interactor = new ImportTextInteractor( DatabaseRepository, YamlFileRepository, Presenter );
            var request = new ImportTextRequest();
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }
    }
}
