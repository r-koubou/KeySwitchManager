namespace ArticulationManager.Domain.Services
{
    public interface IDataTranslationService<in TSource, out TTarget>
    {
        TTarget Translate( TSource source );
    }
}