using ArticulationManager.UseCases.KeySwitches.Removing;

namespace ArticulationManager.Presenters.KeySwitches
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