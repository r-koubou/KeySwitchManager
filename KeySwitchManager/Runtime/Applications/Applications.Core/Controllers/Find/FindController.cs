using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace Application.Core.Controllers.Find
{
    public class FindController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private IFindPresenter Presenter { get; }

        private string DeveloperName { get; }
        private string ProductName { get; }
        private string InstrumentName { get; }

        #region Ctor
        public FindController(
            IKeySwitchRepository databaseRepository,
            IFindPresenter presenter,
            string developerName,
            string productName,
            string instrumentName )
        {
            DatabaseRepository = databaseRepository;
            Presenter          = presenter;
            DeveloperName      = developerName;
            ProductName        = productName;
            InstrumentName     = instrumentName;
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
            var interactor = new FindInteractor( DatabaseRepository, Presenter );
            var request = new FindRequest( DeveloperName, ProductName, InstrumentName );
            var response = interactor.Execute( request );

            Presenter.Complete( response );
        }
    }
}
