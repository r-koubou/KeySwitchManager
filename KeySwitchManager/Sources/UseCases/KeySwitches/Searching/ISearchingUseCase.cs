namespace KeySwitchManager.UseCases.KeySwitches.Searching
{
    public interface ISearchingUseCase
    {
        public SearchingResponse Execute( SearchingRequest request );
    }
}