namespace KeySwitchManager.UseCases.VstExpressionMap.Exporting
{
    public interface IExportingVstExpressionMapUseCase
    {
        public ExportingVstExpressionMapResponse Execute( ExportingVstExpressionMapRequest request );
    }
}