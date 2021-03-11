namespace KeySwitchManager.Domain.Commons
{
    public interface IDictionaryKey<out TKey>
    {
        public TKey Value { get; }
    }
}