using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Core.Applications.Controllers.Delete
{
    public class DeleteController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private IDeletePresenter Presenter { get; }

        private string DeveloperName { get; }
        private string ProductName { get; }
        private string InstrumentName { get; }

        #region Ctor
        public DeleteController(
            IKeySwitchRepository databaseRepository,
            IDeletePresenter presenter,
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
            var interactor = new DeleteInteractor( DatabaseRepository, Presenter );
            var request = new DeleteRequest( DeveloperName, ProductName, InstrumentName );
            var response = interactor.Execute( request );

            if( response.RemovedCount > 0 )
            {
                Presenter.Present( $"{response.RemovedCount} records deleted from database" );
            }
            else
            {
                Presenter.Present( "records not found" );
            }

            Presenter.Complete( response );
        }
    }
}
