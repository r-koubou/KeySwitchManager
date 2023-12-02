using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class DumpController
    {
        public void Dump(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IDumpPresenter presenter )
            => DumpAsync( repository, strategy, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task DumpAsync(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IDumpPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            IDumpUseCase interactor = new DumpInteractor(
                repository,
                strategy,
                presenter
            );

            await interactor.HandleAsync( UnitInputData.Default, cancellationToken );
        }
    }
}
