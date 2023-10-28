namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportContentWriterFactory<in TSource>
    {
        IExportContentWriter Create( TSource source );
    }
}
