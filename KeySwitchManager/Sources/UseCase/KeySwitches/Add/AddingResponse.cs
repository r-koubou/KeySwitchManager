namespace KeySwitchManager.UseCase.KeySwitches.Add
{
    public class AddingResponse
    {
        public bool Result { get; }

        public AddingResponse( bool result )
        {
            Result = result;
        }
    }
}