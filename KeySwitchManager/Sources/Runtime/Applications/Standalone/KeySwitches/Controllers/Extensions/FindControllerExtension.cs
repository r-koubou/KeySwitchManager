﻿using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Applications.Standalone.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.Applications.Standalone.KeySwitches.Controllers.Extensions
{
    public static class FindControllerExtension
    {
        #region Find from local file repository
        public static void Execute(
            this FindController me,
            string databasePath,
            string developerName,
            string productName,
            string instrumentName,
            IFindPresenter presenter )
            => ExecuteAsync( me, databasePath, developerName, productName, instrumentName, presenter ).GetAwaiter().GetResult();

        public static async Task ExecuteAsync(
            this FindController me,
            string databasePath,
            string developerName,
            string productName,
            string instrumentName,
            IFindPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            await me.FindAsync( repository, developerName, productName, instrumentName, presenter, cancellationToken );
        }
        #endregion
    }
}
