using System;

using KeySwitchManager.Commons;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IOutputData<out TData>
    {
        public bool Result { get; }
        public TData Value { get; }
        public Exception? Error { get; }
    }

    public sealed class UnitOutputData : IOutputData<Unit>
    {
        public bool Result => true;
        public Unit Value => Unit.Default;
        public Exception? Error => null;
    }
}
