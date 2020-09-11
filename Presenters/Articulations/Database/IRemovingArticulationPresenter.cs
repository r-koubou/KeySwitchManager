using ArticulationManager.UseCases.Articulations.Database.Removing;

namespace ArticulationManager.Presenters.Articulations.Database
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