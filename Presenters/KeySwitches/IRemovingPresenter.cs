using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IRemovingPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IRemovingPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}