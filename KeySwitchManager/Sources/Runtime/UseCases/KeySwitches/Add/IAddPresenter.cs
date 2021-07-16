using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Add
{
    public interface IAddPresenter : IPresenter<AddingResponse>
    {
        public class Null : IAddPresenter
        {
            public void Complete( AddingResponse response )
            {}
        }
    }
}