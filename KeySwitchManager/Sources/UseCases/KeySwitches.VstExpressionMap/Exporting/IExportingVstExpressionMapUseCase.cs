namespace KeySwitchManager.UseCases.KeySwitches.VstExpressionMap.Exporting
{
    public interface IExportingVstExpressionMapUseCase
    {
        public ExportingVstExpressionMapResponse Execute( ExportingVstExpressionMapRequest request );
    }
}