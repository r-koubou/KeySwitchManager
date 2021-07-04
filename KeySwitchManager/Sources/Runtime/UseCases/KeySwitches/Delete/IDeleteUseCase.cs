namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public interface IDeleteUseCase
    {
        public DeleteResponse Execute( DeleteRequest request );
    }
}