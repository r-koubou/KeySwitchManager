using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class CreateFileInteractor : ICreateFileUseCase
    {
        private ICreateFilePresenter Presenter { get; }

        public CreateFileInteractor() : this( new ICreateFilePresenter.Null() )
        {}

        public CreateFileInteractor( ICreateFilePresenter presenter )
        {
            Presenter = presenter;
        }

        async Task<CreateFileResponse> ICreateFileUseCase.ExecuteAsync( CreateFileRequest request )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            await request.Writer.WriteAsync( new[] { keyswitch }, null );

            return new CreateFileResponse();
        }
    }
}
