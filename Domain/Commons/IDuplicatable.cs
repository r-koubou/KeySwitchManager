namespace ArticulationManager.Domain.Commons
{
    public interface IDuplicatable<T>
    {
        public T Duplicate( T source );
    }
}