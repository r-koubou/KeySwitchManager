using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public sealed class CreateRequest : IRequest<IExportStrategy>
    {
        public IExportStrategy Request { get; }

        public CreateRequest( IExportStrategy strategy )
        {
            Request = strategy;
        }
    }
}
