namespace ArticulationManager.Domain.Translations
{
    public interface IDataTranslator<in TSource, out TTarget>
    {
        TTarget Translate( TSource source );
    }
}