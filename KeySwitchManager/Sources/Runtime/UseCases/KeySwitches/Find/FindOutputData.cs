using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public sealed class FindOutputData : InputData<FindOutputValue>
    {
        public FindOutputData( FindOutputValue value ) : base( value ) {}
    }
}
