namespace KeySwitchManager.Domain.Translators
{
    public interface IDataTranslator<in TSource, out TTarget>
    {
        TTarget Translate( TSource source );
    }
}