namespace KeySwitchManager.Domain.Commons
{
    public interface IDictionaryValue<out TValue>
    {
        public TValue Value { get; }
    }
}