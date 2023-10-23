using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateFileController : IController
    {
        private IKeySwitchWriter Writer { get; }
        private ICreateFilePresenter Presenter { get; }

        public CreateFileController(
            IKeySwitchWriter writer,
            ICreateFilePresenter presenter )
        {
            Writer    = writer;
            Presenter = presenter;
        }

        public void Dispose()
        {
            if( !Writer.LeaveOpen )
            {
                Disposer.Dispose( Writer );
            }
        }

        async Task IController.ExecuteAsync()
        {
            ICreateFileUseCase interactor = new CreateFileInteractor( Presenter );
            var response = await interactor.ExecuteAsync( new CreateFileRequest( Writer ) );
            Presenter.Complete( response );
        }
    }
}
