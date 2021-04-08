namespace KeySwitchManager.UseCases.KeySwitches.StudioOne.Exporting
{
    public interface IExportingStudioOneKeySwitchUseCase
    {
        public ExportingStudioOneKeySwitchResponse Execute( ExportingStudioOneKeySwitchRequest request );
    }
}