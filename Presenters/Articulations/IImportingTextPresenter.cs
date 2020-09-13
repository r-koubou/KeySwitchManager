using ArticulationManager.UseCases.Articulations.Importing.Text;

namespace ArticulationManager.Presenters.Articulations
{
    public interface IImportingTextPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IImportingTextPresenter
        {
            public void Output( OutputData outputData )
            {}
        }

        public class Console : IImportingTextPresenter
        {
            public void Output( OutputData outputData )
            {
            }
        }

    }
}