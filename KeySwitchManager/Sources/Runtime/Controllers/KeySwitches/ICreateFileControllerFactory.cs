using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Controllers.KeySwitches.Create
{
    public interface ICreateFileControllerFactory
    {
        IController Create( string outputFilePath, ICreatePresenter presenter );
    }
}
