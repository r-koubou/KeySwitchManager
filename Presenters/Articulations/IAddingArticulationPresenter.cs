using ArticulationManager.UseCases.Articulations.Adding;

namespace ArticulationManager.Presenters.Articulations
{
    public interface IAddingArticulationPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IAddingArticulationPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}