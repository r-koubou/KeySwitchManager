using KeySwitchManager.Commons;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IInputData<out TData>
    {
        TData Value { get; }
    }

    public abstract class InputData<TData> : IInputData<TData>
    {
        public virtual TData Value { get; }

        protected InputData( TData value )
        {
            Value = value;
        }
    }

    public sealed class UnitInputData : IInputData<Unit>
    {
        public static readonly UnitInputData Default = new();

        public Unit Value => Unit.Default;

        private UnitInputData() {}
    }
}
