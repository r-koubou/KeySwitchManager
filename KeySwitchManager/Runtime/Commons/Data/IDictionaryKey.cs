namespace KeySwitchManager.Commons.Data
{
    public interface IDictionaryKey<out TKey>
    {
        public TKey Value { get; }
    }
}