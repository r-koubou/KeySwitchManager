using ArticulationManager.UseCases.KeySwitches.Importing.Text;

namespace ArticulationManager.Presenters.KeySwitches
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