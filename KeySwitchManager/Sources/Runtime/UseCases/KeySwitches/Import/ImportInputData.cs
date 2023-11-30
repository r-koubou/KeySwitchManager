using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public sealed class ImportInputData : InputData<ImportInputValue>
    {
        public ImportInputData( ImportInputValue value ) : base( value ) {}
    }
}
