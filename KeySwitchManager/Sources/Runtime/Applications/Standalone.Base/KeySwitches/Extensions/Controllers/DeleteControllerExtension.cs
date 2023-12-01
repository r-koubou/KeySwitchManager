using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Extensions.Controllers
{
    public static class DeleteControllerExtension
    {
        public static void Execute( this DeleteController me, string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
            => ExecuteAsync( me, databasePath, developer, product, instrument, logTextView ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync( this DeleteController me, string databasePath, string developer, string product, string instrument, ILogTextView logTextView, CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );

            await me.ExecuteAsync( repository, developer, product, instrument, logTextView, cancellationToken );
        }
    }
}
