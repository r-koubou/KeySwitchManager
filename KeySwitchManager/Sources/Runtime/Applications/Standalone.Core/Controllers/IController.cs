using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        Task ExecuteAsync( CancellationToken cancellationToken = default );
    }
}
