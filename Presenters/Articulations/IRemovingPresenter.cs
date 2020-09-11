using ArticulationManager.UseCases.Articulations.Removing;

namespace ArticulationManager.Presenters.Articulations
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