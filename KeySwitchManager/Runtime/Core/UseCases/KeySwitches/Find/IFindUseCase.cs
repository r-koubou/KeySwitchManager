namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public interface IFindUseCase
    {
        public FindResponse Execute( FindRequest request );
    }
}