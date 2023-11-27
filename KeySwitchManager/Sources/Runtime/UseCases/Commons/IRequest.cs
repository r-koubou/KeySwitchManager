using KeySwitchManager.Commons;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IRequest<out TRequest>
    {
        TRequest Request { get; }
    }

    public sealed class UnitRequest : IRequest<Unit>
    {
        public Unit Request => Unit.Default;
    }
}
