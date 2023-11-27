using System;

using KeySwitchManager.Commons;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IResponse<out TResponse>
    {
        public bool Result { get; }
        public TResponse Response { get; }
        public Exception? Error { get; }
    }

    public sealed class UnitResponse : IResponse<Unit>
    {
        public bool Result => true;
        public Unit Response => Unit.Default;
        public Exception? Error => null;
    }
}
