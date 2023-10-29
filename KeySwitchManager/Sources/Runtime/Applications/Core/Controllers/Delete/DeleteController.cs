using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Core.Controllers.Delete
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

        async Task IController.ExecuteAsync()
        {
            IDeleteUseCase interactor = new DeleteInteractor( DatabaseRepository, Presenter );
            var request = new DeleteRequest( DeveloperName, ProductName, InstrumentName );
            var response = await interactor.ExecuteAsync( request );

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
