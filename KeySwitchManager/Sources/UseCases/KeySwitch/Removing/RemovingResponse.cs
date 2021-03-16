namespace KeySwitchManager.UseCases.KeySwitch.Removing
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