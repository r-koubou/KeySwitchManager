namespace KeySwitchManager.Commons.Data
{
    public interface IDictionaryValue<out TValue>
    {
        public TValue Value { get; }
    }
}