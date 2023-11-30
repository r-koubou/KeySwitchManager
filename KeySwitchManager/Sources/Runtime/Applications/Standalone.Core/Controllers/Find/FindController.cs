using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Find
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

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            IFindUseCase interactor = new FindInteractor( DatabaseRepository, Presenter );
            var inputValue = new FindInputValue( DeveloperName, ProductName, InstrumentName );
            var inputData = new FindInputData( inputValue );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
