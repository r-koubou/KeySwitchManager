using ArticulationManager.UseCases.Articulations.Removing;

namespace ArticulationManager.Presenters.Articulations
{
    public interface IRemovingArticulationPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IRemovingArticulationPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}