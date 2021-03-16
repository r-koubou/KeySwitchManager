namespace KeySwitchManager.UseCases.KeySwitch.Removing
{
    public interface IRemovingUseCase
    {
        public RemovingResponse Execute( RemovingRequest request );
    }
}