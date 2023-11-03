using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.UseCase.KeySwitches.Create;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class CreateInteractor : ICreateUseCase
    {
        private IExportStrategy Strategy { get; }
        private ICreatePresenter Presenter { get; }

        public CreateInteractor( IExportStrategy strategy, ICreatePresenter presenter )
        {
            Strategy   = strategy;
            Presenter  = presenter;
        }

        public async Task<CreateResponse> ExecuteAsync( CancellationToken cancellationToken )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            await Strategy.ExportAsync( new[] { keyswitch }, cancellationToken );

            return new CreateResponse();
        }
    }
}

