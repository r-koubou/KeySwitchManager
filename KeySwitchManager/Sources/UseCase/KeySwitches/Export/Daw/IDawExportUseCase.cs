namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public interface IDawExportUseCase
    {
        public DawExportResponse Execute( DawExportRequest request );
    }
}