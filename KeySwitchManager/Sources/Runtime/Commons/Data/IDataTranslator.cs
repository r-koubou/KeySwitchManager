namespace KeySwitchManager.Commons.Data
{
    public interface IDataTranslator<in TSource, out TTarget>
    {
        TTarget Translate( TSource source );
    }
}