using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public class DeleteInputData : IInputData<DeleteInputValue>
    {
        public DeleteInputValue Value { get; }

        public DeleteInputData( DeleteInputValue value )
        {
            Value = value;
        }
    }
}
