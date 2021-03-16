namespace KeySwitchManager.UseCases.KeySwitch.Adding
{
    public interface IAddingUseCase
    {
        public KeySwitchAddingResponse Execute( KeySwitchAddingRequest request );
    }
}