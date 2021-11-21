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

        public CreateFileResponse Execute( CreateFileRequest request )
        {
            var keyswitch = KeySwitchFactoryHelper.CreateTemplate();
            request.Writer.Write( new[] { keyswitch } );

            return new CreateFileResponse();
        }
    }
}
