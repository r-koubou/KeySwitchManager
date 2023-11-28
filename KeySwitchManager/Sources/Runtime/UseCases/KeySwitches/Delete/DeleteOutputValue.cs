namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public class DeleteOutputValue
    {
        public int RemovedCount { get; }

        public DeleteOutputValue( int removedCount )
        {
            RemovedCount = removedCount;
        }
    }
}
