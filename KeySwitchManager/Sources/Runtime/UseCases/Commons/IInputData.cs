using KeySwitchManager.Commons;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IInputData<out TData>
    {
        TData Value { get; }
    }

    public sealed class UnitInputData : IInputData<Unit>
    {
        public Unit Value => Unit.Default;
    }
}
