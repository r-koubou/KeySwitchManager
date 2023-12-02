using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Base.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Standalone.Base.KeySwitches.Extensions.Controllers
{
    public static class DeleteControllerExtension
    {
        public static void Execute(
            this DeleteController me,
            string databasePath,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter )
            => ExecuteAsync( me, databasePath, developer, product, instrument, presenter ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
            this DeleteController me,
            string databasePath,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter,
            CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );

            await me.ExecuteAsync( repository, developer, product, instrument, presenter, cancellationToken );
        }
    }
}
