using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public sealed class CreateInputData : IInputData<IExportStrategy>
    {
        public IExportStrategy Value { get; }

        public CreateInputData( IExportStrategy strategy )
        {
            Value = strategy;
        }
    }
}
