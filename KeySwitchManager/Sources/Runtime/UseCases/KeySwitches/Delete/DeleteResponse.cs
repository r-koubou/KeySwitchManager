namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public class DeleteResponse
    {
        public int RemovedCount { get; }

        public DeleteResponse( int removedCount )
        {
            RemovedCount = removedCount;
        }
    }
}