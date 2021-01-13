namespace KeySwitchManager.UseCases.KeySwitches.Adding
{
    public class KeySwitchAddingResponse
    {
        public bool Result { get; }

        public KeySwitchAddingResponse( bool result )
        {
            Result = result;
        }
    }
}