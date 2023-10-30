using System;
using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Applications.Core.Controllers
{
    public interface IController : IDisposable
    {
        void Execute()
            => ExecuteAsync( default ).GetAwaiter().GetResult();

        Task ExecuteAsync( CancellationToken cancellationToken );
    }
}
