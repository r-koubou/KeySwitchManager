namespace KeySwitchManager.UseCases.KeySwitches.Removing
{
    public interface IRemovingUseCase
    {
        public RemovingResponse Execute( RemovingRequest request );
    }
}