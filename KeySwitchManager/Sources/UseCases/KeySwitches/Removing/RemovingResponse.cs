namespace KeySwitchManager.UseCases.KeySwitches.Removing
{
    public class RemovingResponse
    {
        public int RemovedCount { get; }

        public RemovingResponse( int removedCount )
        {
            RemovedCount = removedCount;
        }
    }
}