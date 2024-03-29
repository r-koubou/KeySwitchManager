﻿using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class FindController
    {
        public void Find(
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            IFindPresenter presenter )
            => FindAsync( repository, developerName, productName, instrumentName, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task FindAsync(
            IKeySwitchRepository repository,
            string developerName,
            string productName,
            string instrumentName,
            IFindPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            IFindUseCase interactor = new FindInteractor( repository, presenter );
            var inputValue = new FindInputValue( developerName, productName, instrumentName );
            var inputData = new FindInputData( inputValue );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}
