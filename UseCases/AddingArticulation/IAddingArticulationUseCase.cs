namespace ArticulationManager.UseCases.AddingArticulation
{
    public interface IAddingArticulationUseCase : IUseCase<InputData>
    {
        public void Execute( InputData inputData );
    }
}