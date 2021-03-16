namespace KeySwitchManager.UseCases.KeySwitch.Searching
{
    public interface ISearchingUseCase
    {
        public SearchingResponse Execute( SearchingRequest request );
    }
}