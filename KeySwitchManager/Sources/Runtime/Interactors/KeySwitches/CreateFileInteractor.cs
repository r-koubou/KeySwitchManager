using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class CreateFileInteractor : ICreateFileUseCase
    {
        private IExportStrategy Strategy { get; }
        private ICreateFilePresenter Presenter { get; }

        public CreateFileInteractor( IExportStrategy strategy, ICreateFilePresenter presenter )
        {
            Strategy   = strategy;
            Presenter  = presenter;
        }

        async Task<CreateFileResponse> ICreateFileUseCase.ExecuteAsync( CancellationToken cancellationToken )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            await Strategy.ExportAsync( new[] { keyswitch }, cancellationToken );

            return new CreateFileResponse();
        }
    }
}
