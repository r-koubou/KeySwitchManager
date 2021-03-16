namespace KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Exporting
{
    public interface IExportingVstExpressionMapUseCase
    {
        public ExportingVstExpressionMapResponse Execute( ExportingVstExpressionMapRequest request );
    }
}