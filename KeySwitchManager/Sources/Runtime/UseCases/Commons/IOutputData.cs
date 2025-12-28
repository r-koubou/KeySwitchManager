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

    public abstract class OutputData<TData> : IOutputData<TData>
    {
        public virtual bool Result { get; }
        public virtual TData Value { get; }
        public virtual Exception? Error { get; }

        protected OutputData( bool result, TData value, Exception? error = null )
        {
            Result = result;
            Value  = value;
            Error  = error;
        }
    }

    public sealed class UnitOutputData : IOutputData<Unit>
    {
        public static readonly UnitOutputData Default = new();

        public bool Result => true;
        public Unit Value => Unit.Default;
        public Exception? Error => null;

        private UnitOutputData() {}
    }
}
