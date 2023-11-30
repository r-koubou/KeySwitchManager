using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface IController : IDisposable
    {
        void Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        Task ExecuteAsync( CancellationToken cancellationToken = default );
    }
}
