namespace KeySwitchManager.UseCases.KeySwitches.Removing
{
    public class RemovingResponse
    {
        public bool Result { get; }

        public RemovingResponse( bool result )
        {
            Result = result;
        }
    }
}