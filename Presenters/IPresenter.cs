using ArticulationManager.UseCases;

namespace ArticulationManager.Presenters
{
    public interface IPresenter<in TOutputData> where TOutputData : IOutputData
    {
        public void Output( TOutputData outputData );
    }
}