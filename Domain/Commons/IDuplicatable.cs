namespace KeySwitchManager.Domain.Commons
{
    public interface IDuplicatable<T>
    {
        public T Duplicate( T source );
    }
}