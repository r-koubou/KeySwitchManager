namespace KeySwitchManager.UseCase.KeySwitches.Add
{
    public interface IAddUseCase
    {
        public AddingResponse Execute( AddRequest request );
    }
}