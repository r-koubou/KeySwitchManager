using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public sealed class ExportInputData : InputData<ExportInputValue>
    {
        public ExportInputData( ExportInputValue value ) : base( value ) {}
    }
}
