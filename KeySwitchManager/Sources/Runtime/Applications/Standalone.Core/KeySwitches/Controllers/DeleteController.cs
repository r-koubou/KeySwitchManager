using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers
{
    public class DeleteController
    {
        public void Execute( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
            => ExecuteAsync( databasePath, developer, product, instrument, logTextView ).GetAwaiter().GetResult();

        public async Task ExecuteAsync( string databasePath, string developer, string product, string instrument, ILogTextView logTextView, CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new DeletePresenter( logTextView );
            var interactor = new DeleteInteractor( repository, presenter );
            var inputData = new DeleteInputData( new DeleteInputValue( developer, product, instrument ) );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
