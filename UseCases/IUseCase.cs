namespace ArticulationManager.UseCases
{
    public interface IUseCase<in TInput> where TInput : IInputData
    {
        public void Execute( TInput inputData );
    }
}