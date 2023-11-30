using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Delete
{
    public sealed class DeleteController : IController
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

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            IDeleteUseCase interactor = new DeleteInteractor( DatabaseRepository, Presenter );
            var request = new DeleteInputData( new DeleteInputValue( DeveloperName, ProductName, InstrumentName ) );

            await interactor.HandleAsync( request, cancellationToken );
        }
    }
}
