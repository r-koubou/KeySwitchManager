using KeySwitchManager.UseCases.KeySwitches.Adding;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IAddingPresenter : IPresenter<KeySwitchAddingResponse>
    {
        public class Null : IAddingPresenter
        {
            public void Complete( KeySwitchAddingResponse response )
            {}
        }
    }
}