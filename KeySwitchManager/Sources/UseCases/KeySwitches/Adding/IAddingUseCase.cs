namespace KeySwitchManager.UseCases.KeySwitches.Adding
{
    public interface IAddingUseCase
    {
        public KeySwitchAddingResponse Execute( KeySwitchAddingRequest request );
    }
}