using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Base.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;

namespace KeySwitchManager.Applications.Standalone.Base.KeySwitches.Extensions.Controllers
{
    public static class DeleteControllerExtension
    {
        public static void DeleteFromLocalDatabase(
            this DeleteController me,
            string databasePath,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter )
            => DeleteFromLocalDatabaseAsync( me, databasePath, developer, product, instrument, presenter ).GetAwaiter().GetResult();

        public static async Task DeleteFromLocalDatabaseAsync(
            this DeleteController me,
            string databasePath,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter,
            CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );

            await me.DeleteAsync( repository, developer, product, instrument, presenter, cancellationToken );
        }
    }
}
