using ArticulationManager.UseCases.Articulations.Adding;

namespace ArticulationManager.Presenters.Articulations
{
    public interface IAddingPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IAddingPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}