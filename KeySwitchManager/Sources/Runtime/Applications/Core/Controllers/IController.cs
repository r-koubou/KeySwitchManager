using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        Task ExecuteAsync( CancellationToken cancellationToken = default );
    }
}
