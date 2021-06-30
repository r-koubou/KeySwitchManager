namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public interface IExportDawUseCase
    {
        public ExportDawResponse Execute( ExportDawRequest request );
    }
}